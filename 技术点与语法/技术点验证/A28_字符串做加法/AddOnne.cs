namespace 技术点验证.A28_字符串做加法
{
    public static class AddOnne
    {
        /// <summary>
        /// 实现字符串的加法运算
        /// </summary>
        /// <param name="addOne"></param>
        /// <param name="addTwo"></param>
        /// <returns></returns>
        public static string StringAddition(string addOne, string addTwo)
        {
            bool inc = false;//是否进位
            char[] addOneArray = addOne.ToCharArray();
            Array.Reverse(addOneArray);
            char[] addTwoArray = addTwo.ToCharArray();
            Array.Reverse(addTwoArray);
            int addOneLen = addOneArray.Length;
            int addTwoLen = addTwoArray.Length;
            int maxLen = Math.Max(addOneLen, addTwoLen) + 1;

            char[] result = new char[maxLen];
            Stack<char> charStack = new Stack<char>();
            for(int i = 0 ; i < maxLen ; i++)
            {
                charStack.Clear();
                bool addOneCalc = addOneLen > i;
                bool addTwoCalc = addTwoLen > i;
                if(inc)
                {
                    charStack.Push('1');
                }
                if(addTwoCalc)
                {
                    charStack.Push(addOneArray[i]);
                }
                if(addOneCalc)
                {
                    charStack.Push(addTwoArray[i]);
                }
                char tempResultChar = '0';
                while(0 != charStack.Count)
                {
                    char calcChar = charStack.Pop();
                    inc = false;
                    tempResultChar = CharAdd(tempResultChar, calcChar, out inc);
                    result[i] = tempResultChar;
                }
            }
            Array.Reverse(result);
            string stringResult = new string(result);
            return stringResult.TrimStart('\0');
        }

        /// <summary>
        /// 实现一个字符的加法
        /// </summary>
        /// <param name="addend"></param>
        /// <param name="augend"></param>
        /// <param name="increase"></param>
        /// <returns></returns>
        public static char CharAdd(char addend, char augend, out bool increase)
        {
            increase = false;
            char result;
            int addendNum = addend - '0';
            int augendNum = augend - '0';
            int resultNum = addendNum + augendNum;
            if(resultNum > 9)
            {
                increase = true;
                resultNum = resultNum - 10;
            }

            result = (char)(resultNum + '0');
            return result;
        }
    }
}