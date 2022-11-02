using System.IO;
using System.Linq;
using System.Text;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace Mykhailo {
    internal class Program {
        static void Main(string[] args) {
            File.WriteAllText("result.txt", String.Empty);
            File.WriteAllText("accounts_are_not_in_the_file_of_millions.txt", String.Empty);
            File.WriteAllText("resultstrings.txt", String.Empty);
            var thousandsRead = File.ReadLines("thousands.txt");
            var millionsRead = File.ReadLines("millions.txt");
            string firstNickname;
            string secondNickname = thousandsRead.ElementAt(1);
            int SecondNicknameIndex = 1;
            for (int i = 0; i < thousandsRead.Count() - 1; i++) {
                if (SecondNicknameIndex <= 0) {
                    firstNickname = thousandsRead.ElementAt(i-1);
                } else {
                    firstNickname = thousandsRead.ElementAt(i);
                }
                secondNickname = thousandsRead.ElementAt(i + 1);
                int FirstNicknameIndex = GetNicknameIndex(firstNickname, millionsRead);
                SecondNicknameIndex = GetNicknameIndex(secondNickname, millionsRead);
                if(SecondNicknameIndex == 0) {
                    File.AppendAllText("accounts_are_not_in_the_file_of_millions.txt", secondNickname + Environment.NewLine);
                    secondNickname = thousandsRead.ElementAt(i + 2);
                    SecondNicknameIndex = GetNicknameIndex(secondNickname, millionsRead);
                }
                int differenceBetweenNicknames = SecondNicknameIndex - FirstNicknameIndex;
                if (differenceBetweenNicknames >= 270) {
                    for (int j = FirstNicknameIndex + 1; j < SecondNicknameIndex; j++) {
                        if(j == FirstNicknameIndex + 1) {
                            File.AppendAllText("resultstrings.txt", FirstNicknameIndex + "-" + SecondNicknameIndex);
                        }
                        File.AppendAllText("resultstrings.txt", Environment.NewLine);
                        File.AppendAllText("result.txt", millionsRead.ElementAt(j) + Environment.NewLine);
                    }
                }
            }
            static int GetNicknameIndex(string nickname, IEnumerable<string>? millionsRead) {
                if (millionsRead == null) {
                    return -1;
                }
                int indexOfNickname = -1;
                string[] readText = File.ReadAllLines("millions.txt");
                string[] readAccounts = File.ReadAllLines("accounts_are_not_in_the_file_of_millions.txt");
                indexOfNickname = Array.IndexOf(readText, nickname);
                if (indexOfNickname == -1 && !readAccounts.Contains(nickname)) {
                    File.AppendAllText("accounts_are_not_in_the_file_of_millions.txt", nickname + Environment.NewLine);
                }
                return indexOfNickname;
            }
        }
    }
}