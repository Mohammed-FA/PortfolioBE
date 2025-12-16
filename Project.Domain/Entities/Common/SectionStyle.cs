namespace Project.Domain.Entities.Common
{
    public class SectionStyle
    {
        // Box Model والخلفية
        public string? MarginMode { get; set; } // "all" | "custom"
        public string? PaddingMode { get; set; } // "all" | "custom"
        public int? Padding { get; set; }
        public int? PaddingTop { get; set; }
        public int? PaddingBottom { get; set; }
        public int? PaddingLeft { get; set; }
        public int? PaddingRight { get; set; }
        public int? Margin { get; set; }
        public int? MarginTop { get; set; }
        public int? MarginBottom { get; set; }
        public int? MarginLeft { get; set; }
        public int? MarginRight { get; set; }

        // Background
        public string? BackgroundColor { get; set; }
        public string? BackgroundImage { get; set; }

        // Border
        public int? BorderRadius { get; set; }
        public string? BoxShadow { get; set; }
        public string? Border { get; set; }
        public string? BorderTop { get; set; }
        public string? BorderBottom { get; set; }
        public int? BorderWidth { get; set; }
        public string? BorderColor { get; set; }
        public string? BorderStyle { get; set; }

        // Opacity
        public int? Opacity { get; set; }

        // Fonts & Text
        public string? Color { get; set; }
        public int? FontSize { get; set; }
        public string? FontWeight { get; set; } // يمكن string أو number، نتركه string
        public string? TextAlign { get; set; } // "left" | "right" | "center" | "justify"
        public string? FontFamily { get; set; }
        public string? FontStyle { get; set; }
        public float? LineHeight { get; set; }
        public int? LetterSpacing { get; set; }
        public string? TextShadow { get; set; }
        public string? TextDecoration { get; set; }
        public string? WhiteSpace { get; set; }
        public string? OverflowWrap { get; set; }

        // Size
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? MinWidth { get; set; }
        public int? MaxWidth { get; set; }
        public int? MinHeight { get; set; }
        public int? MaxHeight { get; set; }

        // Flex/Grid/Layout
        public string? Display { get; set; } // "flex" | "block" | ...
        public string? JustifyContent { get; set; } // flex-start | ...
        public string? AlignItems { get; set; } // flex-start | ...
        public string? AlignContent { get; set; }
        public int? Gap { get; set; }
        public string? FlexDirection { get; set; }
        public string? JustifyItems { get; set; }

        // Position & Overflow
        public string? Overflow { get; set; } // visible | hidden | scroll | auto
        public string? OverflowX { get; set; }
        public string? OverflowY { get; set; }
        public string? Position { get; set; } // relative | absolute | ...
        public int? Top { get; set; }
        public int? Left { get; set; }
        public int? Right { get; set; }
        public int? Bottom { get; set; }
        public int? ZIndex { get; set; }
        public string? Cursor { get; set; }
        public string? Transition { get; set; }
        public string? Transform { get; set; }

        // Grid specific
        public string? GridTemplateRows { get; set; }
        public string? GridTemplateColumns { get; set; }

        // Background extras
        public string? BackgroundRepeat { get; set; }
        public string? BackgroundSize { get; set; }
        public string? BackgroundPosition { get; set; }
    }

}
