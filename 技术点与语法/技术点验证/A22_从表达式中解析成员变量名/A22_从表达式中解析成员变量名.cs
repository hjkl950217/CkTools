using System.Linq.Expressions;
using System.Reflection;

namespace 技术点验证
{
    [VerifcationType(VerificationTypeEnum.A22_从表达式中解析成员变量名)]
    public class A22_从表达式中解析成员变量名 : IVerification
    {
        public void Start(string[]? args)
        {
            Type typeA = typeof(A22TestEntity_A);
            Type typeB = typeof(A22TestEntity_B);
            Type typeC = typeof(A22TestEntity_C);

            ParameterExpression x = Expression.Parameter(typeA, "x"); //  --> (A22TestEntity_A x)
            MemberInfo? memberInfo_TestBObject = typeA.GetProperty(nameof(A22TestEntity_A.TestBObject));//获取 TestBObject 的类型信息
            MemberExpression member_AtoB = Expression.MakeMemberAccess(x, memberInfo_TestBObject);// -->a.TestBObject

            MemberInfo? memberInfo_TestCObject = typeB.GetProperty(nameof(A22TestEntity_B.TestCObject));//获取 TestCObject 的类型信息
            MemberExpression member_AtoBToC = Expression.MakeMemberAccess(member_AtoB, memberInfo_TestCObject);// -->a.TestBObject.TestCObject

            MemberInfo? memberInfo_CName = typeC.GetProperty(nameof(A22TestEntity_C.Name));//获取 Name 的类型信息
            MemberExpression member_AtoBToCToName = Expression.MakeMemberAccess(member_AtoBToC, memberInfo_CName);// -->a.TestBObject.TestCObject.Name

            LambdaExpression lambdaExpression = Expression.Lambda(member_AtoBToCToName, x);

            string? result = this.ResolvePropertyName((A22TestEntity_A a) => a.TestBObject.TestCObject.Name);
            Console.WriteLine(result);
        }

        public string? ResolvePropertyName<T, TProperty>(Expression<Func<T, TProperty>> memberAccessor)
        {
            Func<Type, MemberInfo?, LambdaExpression, string?>? resolver = this.GetPropertyNameResolver();

            string? propertyName = resolver(typeof(T), this.GetMember(memberAccessor), memberAccessor);
            return propertyName;
        }

        public Func<Type, MemberInfo?, LambdaExpression, string?> GetPropertyNameResolver()
        {
            return new Func<Type, MemberInfo?, LambdaExpression, string?>(this.DefaultPropertyNameResolver);
        }

        public MemberInfo? GetMember<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            MemberExpression? memberExpression = RemoveUnary(expression.Body) as MemberExpression;
            if(memberExpression == null)
            {
                return null;
            }
            Expression expression2 = memberExpression.Expression;
            while(true)
            {
                expression2 = RemoveUnary(expression2);
                if(expression2 == null || expression2.NodeType != ExpressionType.MemberAccess)
                {
                    break;
                }
                expression2 = ((MemberExpression)expression2).Expression;
            }
            if(expression2 == null || expression2.NodeType != ExpressionType.Parameter)
            {
                return null;
            }
            return memberExpression.Member;
        }

        private static Expression RemoveUnary(Expression toUnwrap)
        {
            if(toUnwrap is UnaryExpression)
            {
                return ((UnaryExpression)toUnwrap).Operand;
            }
            return toUnwrap;
        }

        private string? DefaultPropertyNameResolver(Type type, MemberInfo? memberInfo, LambdaExpression expression)
        {
            if(expression != null)
            {
                PropertyChain propertyChain = PropertyChain.FromExpression(expression);
                if(propertyChain.Count > 0)
                {
                    return propertyChain.ToString();
                }
            }
            return memberInfo?.Name;
        }
    }
}