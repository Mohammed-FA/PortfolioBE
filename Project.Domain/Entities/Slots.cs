using System.ComponentModel.DataAnnotations.Schema;
using Project.Domain.Entities.Common;
using Project.Domain.Enum;

namespace Project.Domain.Entities
{
    public class Slots : SectionStyle
    {

        public int Id { get; set; }

        public int ColumnId { get; set; }
        public string? Url { get; set; }
        public string? Label { get; set; }
        public string? Content { get; set; }
        public string? Href { get; set; }
        public ComponentType Type { get; set; }
        public string? ListStyleType { get; set; }


        public string? LinkType { get; set; }
        public string? Target { get; set; }
        public string? Orientation { get; set; }

        public int? Thickness { get; set; }
        public string? IconName { get; set; }
        public string? Poster { get; set; }

        public double? Volume { get; set; }
        public bool? PlaybackRate { get; set; }
        public bool? Controls { get; set; }
        public bool? Muted { get; set; }
        public bool? Loop { get; set; }
        public bool? Autoplay { get; set; }
        public ICollection<ListItems>? Items { get; set; }
        [ForeignKey(nameof(ColumnId))]
        public Columns? columns { get; set; }
    }
    public class ListItems : SectionStyle
    {
        public int Id { get; set; }

        public string? label { get; set; }

        public string? IconName { get; set; }
        public string? Href { get; set; }
        public string? LinkType { get; set; }
        public string? Target { get; set; }
        public int SlotsId { get; set; }

        [ForeignKey(nameof(SlotsId))]
        public Slots? Slots { get; set; }
    }
}
