using System.Text;

namespace Bird.Client.Mtchmkr.Portable.Common
{
    public class PasswordConstraint
    {

        public bool AllowNull { get; set; } = true;
        private bool m_RequiresUpperCase;
        public bool RequiresUpperCase
        {
            get { return (AllowNull ? false : m_RequiresUpperCase); }
            set { m_RequiresUpperCase = value; }
        }
        private bool m_RequiresLowerCase = true;
        public bool RequiresLowerCase
        {
            get { return (AllowNull ? false : m_RequiresLowerCase); }
            set { m_RequiresLowerCase = value; }
        }
        private bool m_RequiresNumber=true;
        public bool RequiresNumber
        {
            get { return (AllowNull ? false : m_RequiresNumber); }
            set { m_RequiresNumber = value; }
        }
        private bool m_RequiresSpecialCharacter = true;
        public bool RequiresSpecialCharacter
        {
            get { return (AllowNull ? false : m_RequiresSpecialCharacter); }
            set { m_RequiresSpecialCharacter = value; }
        }
        private int m_MinimumLength = 8;
        public int MinimumLength
        {
            get { return (AllowNull ? 0 : m_MinimumLength); }
            set { m_MinimumLength = value; }
        }

        public override string ToString()
        {
            if (AllowNull)
                return "Any characters or length is allowed including empty values";
            string and = "and";
            string[] array= new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
            if (MinimumLength>0)
            {
                array[0] = string.Format("{0} the minimum length of {1}", and , MinimumLength);
                and = ",";
            }
            if (RequiresSpecialCharacter)
            {
                array[1] = string.Format("{0} a special character", and);
                and = ",";
            }
            if (RequiresNumber)
            {
                array[2] = string.Format("{0} a number" , and);
                and = ",";
            }
            if (RequiresUpperCase)
            {
                array[3] = string.Format("{0} an upper case character" , and);
                and = ",";
            }
            if (RequiresLowerCase)
            {
                array[4] = string.Format("{0} a lower case character [a-z]" , and);
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("The password must contain");
            for (int i = array.Length-1; i > - 1; i--)
                sb.Append(array[i]);
            return sb.ToString();
        }
    }
}