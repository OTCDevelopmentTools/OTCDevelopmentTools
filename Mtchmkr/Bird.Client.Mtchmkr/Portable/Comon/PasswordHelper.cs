namespace Bird.Client.Mtchmkr.Portable.Common
{
    public class PasswordResult
    {
        private bool m_Pass = false;
        public bool Pass
        {
            get => m_Pass;
            set
            {
                if (m_Pass) return;
                m_Pass = value;
            }
        }
        public void Reset()
        {
            m_Pass = false;
        }
    }

    internal class PasswordHelper
    {

        internal static bool ValidatePassword(string passWord, PasswordConstraint constraint)
        {
            if (constraint.MinimumLength>0 && passWord.Length< constraint.MinimumLength)
            {
                return false;
            }

            if (constraint.RequiresLowerCase)
            {
                bool lower = false;
                foreach (char c in passWord)
                {
                    if (c >= 'a' && c <= 'z')
                    {
                        lower = true;
                    }
                }
                if (!lower)
                    return false;
            }

            if (constraint.RequiresUpperCase)
            {
                bool upper = false;
                foreach (char c in passWord)
                {
                    if (c >= 'A' && c <= 'Z')
                    {
                        upper = true;
                    }
                }
                if (!upper)
                    return false;
            }

            if (constraint.RequiresNumber)
            { 
                bool number = false;
                foreach (char c in passWord)
                {
                   
                    if (c >= '0' && c <= '9')
                    {
                        number = true;
                    }
                }
                if (!number)
                    return false;
            }

            if (constraint.RequiresSpecialCharacter)
            {
                char[] special = { '@', '#', '$', '%', '^', '&', '+', '=' }; // or whatever    
                if (passWord.IndexOfAny(special) == -1) return false;
            }
            return true;
        }
    }
}