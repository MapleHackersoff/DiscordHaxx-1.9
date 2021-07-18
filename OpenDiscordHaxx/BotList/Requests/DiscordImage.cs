using System;
using System.Drawing;
using System.IO;

namespace DiscordHaxx
{
    /// <summary>
    /// Temporary class until Anarchy has a better DiscordImage class
    /// </summary>
    public class ReverseDiscordImage
    {
        private string _base64;
        public string Base64
        {
            get { return _base64; }
            set 
            {
                _base64 = value;

                if (_base64 == null)
                    Image = null;
                else
                {
                    MemoryStream ms = new MemoryStream(Convert.FromBase64String(value.Split(',')[1]));
                    Image = Image.FromStream(ms);
                    ms.Dispose();
                }
            }
        }

        public Image Image { get; private set; }
    }
}
