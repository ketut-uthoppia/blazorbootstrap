namespace BlazorBootstrap;

public partial class FeatherIcon : BlazorBootstrapComponentBase
{
    #region Properties, Indexers

    protected override string? ClassNames =>
        BuildClassNames(Class,
            (FeatherIconUtility.Icon(), string.IsNullOrWhiteSpace(CustomIconName)),
            (FeatherIconUtility.Icon(Name), string.IsNullOrWhiteSpace(CustomIconName)),
            (CustomIconName!, !string.IsNullOrWhiteSpace(CustomIconName)),
            (FeatherIconUtility.IconSize(Size)!, Size != IconSize.None),
            (Color.ToIconColorClass(), Color != IconColor.None));

    /// <summary>
    /// Gets or sets the icon color.
    /// </summary>
    /// <remarks>
    /// Default value is <see cref="IconColor.None" />.
    /// </remarks>
    [Parameter]
    public IconColor Color { get; set; } = IconColor.None;

    /// <summary>
    /// Gets or sets the custom icon name.
    /// Specify custom icons of your own, like `fontawesome`. Example: `fas fa-alarm-clock`.
    /// </summary>
    /// <remarks>
    /// Default value is null.
    /// </remarks>
    [Parameter]
    public string? CustomIconName { get; set; }

    /// <summary>
    /// Gets or sets the icon name.
    /// </summary>
    /// <remarks>
    /// Default value is <see cref="IconName.None" />.
    /// </remarks>
    [Parameter]
    public FeatherIconName Name { get; set; } = FeatherIconName.None;

    /// <summary>
    /// Gets or sets the icon size.
    /// </summary>
    /// <remarks>
    /// Default value is <see cref="IconSize.None" />.
    /// </remarks>
    [Parameter]
    public IconSize Size { get; set; } = IconSize.None;

    #endregion
}

