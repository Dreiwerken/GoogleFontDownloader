using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleFontDownloader
{
    class GoogleFontsModel
    {
        public IList<GoogleFontEntryModel> familyMetadataList { get; set; }
        public string promotedScript { get; set; }
    }

    class GoogleFontEntryModel
    {
        public string category { get; set; }
        public string dateAdded { get; set; }
        public int defaultSort { get; set; }
        public IList<string> designers { get; set; }
        public string family { get; set; }
        public Dictionary<string, GoogleFontTypeModel> fonts { get; set; }
        public string lastModified { get; set; }
        public int popularity { get; set; }
        public int size { get; set; }
        public IList<string> subsets { get; set; }
        public Dictionary<string, bool> parsedSubsets { get; set; } = new Dictionary<string, bool>(); // not in API
        public int trending { get; set; }
        public bool selected { get; set; } // not in API
    }

    class GoogleFontTypeModel
    {
        public int slant { get; set; }
        public int thickness { get; set; }
        public int width { get; set; }
        public bool selected { get; set; } // not in API
    }
}
