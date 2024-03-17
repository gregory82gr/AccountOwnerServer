namespace AccountOwnerServer.Validation
{
    public class OwnerIdValidation
    {
        private const int startingPartLength = 8;
        private const int secondPartLength = 4;
        private const int thirdPartLength = 4;
        private const int fourthPartLength = 4;
        private const int lastPartLength = 12;

        public bool IsValid(string ownerId)
        {
            if(ownerId.Length!=36) return false;
            
            var firstDelimiter = ownerId.IndexOf('-');
            var secondDelimiter = ownerId.Substring(9).IndexOf('-');
            var thirdDelimiter = ownerId.Substring(14).IndexOf('-');
            var fourthDelimiter = ownerId.Substring(19).IndexOf('-');

            if (firstDelimiter == -1 || (firstDelimiter == secondDelimiter))
                throw new ArgumentException();
            if (secondDelimiter == -1 )
                throw new ArgumentException();
            if (thirdDelimiter == -1)
                throw new ArgumentException();
            if(fourthDelimiter == -1)
                throw new ArgumentException();

            var firstPart = ownerId.Substring(0, firstDelimiter);
            if (firstPart.Length != startingPartLength)
                return false;
            if(!OnlyHexInString(firstPart)) return false;
            
            var secondPart = ownerId.Substring(9, secondDelimiter);
            if (secondPart.Length != secondPartLength)
                return false;
            if (!OnlyHexInString(secondPart)) return false;

            var thirdPart = ownerId.Substring(14, thirdDelimiter);
            if (thirdPart.Length != thirdPartLength)
                return false;
            
            if (!OnlyHexInString(thirdPart)) return false;

            var fourthPart = ownerId.Substring(19, fourthDelimiter);
            if (fourthPart.Length != fourthPartLength)
                return false;

            if (!OnlyHexInString(fourthPart)) return false;
            var lastPart = ownerId.Substring(24);
            if (lastPart.Length != lastPartLength)
                return false;
            if (!OnlyHexInString(lastPart)) return false;
            
            return true;
        }

        public bool OnlyHexInString(string test)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b[0-9a-fA-F]+\b\Z");
        }
    }
}
