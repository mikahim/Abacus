using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLabs.Platform.Services.Media;

namespace Abacus.Interfaces
{
    public interface IParseImage
    {
        Task<bool> parseTextFromImageASync(MediaFile file);

        string returnParsedText();
    }
}
