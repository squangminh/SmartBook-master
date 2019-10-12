using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdwardSoft.API.Mobile.Helper
{
    public class TicketHelper
    {
        #region TicketHash
        public string Hash(int placeId, long userId, string key, bool agency = false)
        {
            var hashString = RandomTicketPrefix(agency);

            hashString += Checksum(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss:ms")) + Checksum(placeId);
            hashString += ChecksumFormat(Checksum(userId)) + RandomTicketCode() + Checksum(key);

            return hashString;
        }

        #endregion

        #region CheckSum
        private int Checksum(long value)
        {
            var _value = value.ToString().Trim();
            return ShardIndex(_value);
        }
        private int Checksum(string value)
        {
            var _value = value.Trim();
            return ShardIndex(_value);
        }
        private int ShardIndex(string _value)
        {
            Random _random = new Random();
            byte[] asciiBytes = Encoding.ASCII.GetBytes(_value);
            var _tableSize = asciiBytes.Length;
            int _sum = 0;
            foreach (var node in asciiBytes)
            {
                _sum += (int)node;
            }
            _sum += _random.Next(10, 100);

            decimal d = (_sum / _tableSize);
            return (int)Math.Round(d, 0);
        }
        #endregion

        #region Ultities
        private string RandomTicketPrefix(bool agency = false)
        {
            if (!agency)
            {
                Random _random = new Random();
                int num = _random.Next(0, 26); // Zero to 25
                char let = (char)('a' + num);
                var ret = let.ToString().ToUpper();

                while (ret == "D")
                {
                    num = _random.Next(0, 26);
                    let = (char)('a' + num);
                    ret = let.ToString().ToUpper();
                }

                return ret;
            }
            else return "D";
        }

        private string RandomTicketCode(int size = 1)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            builder.Append(random.Next(1, 99).ToString("D2"));
            return builder.ToString();
        }

        private string ChecksumFormat(int data)
        {
            return data.ToString("X");
        }

        #endregion
    }
}
