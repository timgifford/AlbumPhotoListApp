using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AlbumServices
{
    [DataContract(Name = "AlbumPhoto")]
    public class AlbumPhoto
    {
        [DataMember(Name = "albumId")]
        public string albumId { get; set; }

        [DataMember(Name = "id")]
        public string id { get; set; }

        [DataMember(Name = "title")]
        public string title { get; set; }
    }
}
