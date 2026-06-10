using System.Diagnostics.CodeAnalysis;
using CkTools;

namespace 技术点验证
{
    /// <summary>
    /// 值对象基类-用于值类型非class<para></para>
    /// 可保证数据不能为null，数据正确(需重写<see cref="BizCheckValue"/>方法以完成业务规则检测)<para></para>
    /// 使用时可以当普通数据使用，如: int a=new Age(50)
    /// </summary>
    /// <remarks>
    /// 微软关于值对象的资料:https://docs.microsoft.com/zh-cn/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects
    /// 其它人关于class为引用类型的处理  https://enterprisecraftsmanship.com/posts/value-object-better-implementation <para></para>
    /// </remarks>
    /// <typeparam name="TValue"></typeparam>
    public abstract class ValueBase<TValue> : IValue<TValue>, IValueCheck<TValue>
        where TValue : notnull
    {
        [NotNull]
        public TValue Value { get; protected set; }

        [NotNull]
        TValue IValue<TValue>.Value { get => this.Value; set => this.Value = value; }

        #region 内部逻辑

        protected ValueBase([NotNull] TValue data)
        {
            this.Value = data;
            this.CheckVlueWithException();
        }

        public override bool Equals(object? obj)
        {
            return obj switch
            {
                object _ when object.ReferenceEquals(this, obj) => true,
                IValue<TValue> obj2 => this.EqualsCode(obj2.Value),
                TValue obj2 => this.EqualsCode(obj2),
                _ => false,
            };
        }

        public override int GetHashCode()
        {
            return this.GetHashCodeCore();
        }

        public override string ToString()
        {
            return this.Value.ToString() ?? string.Empty;
        }

        /// <summary>
        /// 检查空引用。值类型与非空则返回false，引用类型为空时返回true
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool IsNulReference(TValue data)
        {
            if(typeof(TValue).IsValueType)
            {
                return false;
            }
            else
            {
                return data == null;
            }
        }

        #endregion 内部逻辑

        #region 由子类重写

        /// <summary>
        /// 由子类重写，指示如何进行业务检查。true为检查通过，否则为false
        /// </summary>
        /// <returns></returns>
        public abstract bool BizCheckValue();

        #endregion 由子类重写

        #region 子类可重写

        /// <summary>
        /// 子类可重写，指示当业务检查失败时，异常中的错误信息。
        /// </summary>
        /// <param name="value">发生错误的值</param>
        /// <returns></returns>
        public virtual string ErrorMsgForCheckValue(TValue value)
        {
            return $"Check {this.GetType().Name} value error. now value:{value.ToString()}";
        }

        /// <summary>
        /// 子类可重写，指示如果使用<paramref name="value"/>与<see cref="Value"/>进行内容比较<para></para>
        /// 引用、类型比较由基类完成<para></para>
        /// 基类默认行为：用<see cref="object.Equals(object)"/>方法对<paramref name="value"/>、<see cref="Value"/>做相等比较
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual bool EqualsCode(TValue value)
        {
            return object.Equals(this.Value, value);
        }

        /// <summary>
        /// 子类可重写，指示如何计算<see cref="Value"/>的哈希值<para></para>
        /// 基类默认行为：用<see cref="TValue.GetHashCode"/>方法对<see cref="Value"/>做计算
        /// </summary>
        /// <returns></returns>
        protected virtual int GetHashCodeCore()
        {
            return this.Value.GetHashCode();
        }

        #endregion 子类可重写

        #region 子类可用的方法

        /// <summary>
        /// 获取一个<see cref="BizError"/>对象
        /// </summary>
        /// <returns></returns>
        protected virtual BizError GetBizError()
        {
            return new BizError()
            {
                ErrorCode = "0OO0",
                CustomObject = this,
                ErrorMessage = this.ErrorMsgForCheckValue(this.Value)
            };
        }

        #endregion 子类可用的方法

        #region 符号重载

        //== !=必须调用Equals方法。因为 vo1==null 这种也是调用一个等号运算符，导致死循环

        public static bool operator ==(ValueBase<TValue> v1, ValueBase<TValue> v2)
        {
            return object.Equals(v1, v2);//具体比较逻辑由Equals完成
        }

        public static bool operator !=(ValueBase<TValue> v1, ValueBase<TValue> v2)
        {
            return !object.Equals(v1, v2);// 具体比较逻辑由Equals完成
        }

        #region 隐式和显式转换

        /// <summary>
        /// 隐式转换  <![CDATA[ValueObject<T, TValue>->TValue   eg: TValue a=new ValueObject<TValue>(xxx)]]>
        /// </summary>
        /// <param name="valueObject"></param>
        public static implicit operator TValue(ValueBase<TValue> valueObject)
        {
            return valueObject.Value;
        }

        /*
         * 显式转换在这个基础类里没办法写，原因：
         *  TValue b = (TValue)(new ValueObject<TValue>(xxx)); 这种代码在编译时 (TValue)这个强转的就被编译器拿掉了。
         *
         * TValue b = (double)(new ValueObject<TValue>(xxx)); 而这种强转，也不可能在基础类里把所有的都 写进去
         *
         */

        #endregion 隐式和显式转换

        #endregion 符号重载

        void IValue<TValue>.SetValue(TValue value)
        {
            this.Value = value;
        }

        /// <summary>
        /// 检查数据<para></para>
        /// 检查通过返回true，否则返回false+错误对象
        /// </summary>
        /// <returns></returns>
        (bool checkResult, BizError errorObj) IValueCheck<TValue>.Check(TValue value)
        {
            if(this.IsNulReference(this.Value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if(this.BizCheckValue())
            {
                return (
                    true,
                    new BizError());
            }
            else
            {
                return (false, this.GetBizError());
            }
        }

        private void CheckVlueWithException()
        {
            (bool checkResult, BizError errorObj) checkObj = ((IValueCheck<TValue>)this).Check(this.Value);
            if(!checkObj.checkResult)
            {
                throw new ArgumentException(checkObj.errorObj.ErrorMessage, nameof(this.Value));
            }
        }
    }
}