using System.Collections.Generic;
using System.Text;

namespace PAKDial.Common
{
    public static class CommonSpacing
    {
        public static string RemoveSpacestoTrim(string response)
        {
            if(response != null)
            {
                string name = response.Replace(" ", "");
                return name.Trim().ToLower();
            }
            return null;

        }
    }

}
