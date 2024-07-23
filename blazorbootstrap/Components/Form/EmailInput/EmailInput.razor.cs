namespace BlazorBootstrap;

public partial class EmailInput : BlazorBootstrapComponentBase
{
    #region Fields and Constants

    private CultureInfo cultureInfo = default!;

    private FieldIdentifier fieldIdentifier;

    private string step = default!;

    #endregion

    #region Methods

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var currentValue = Value; // object

            if (currentValue is null)
                Value = default!;

            await ValueChanged.InvokeAsync(Value);
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    protected override async Task OnInitializedAsync()
    {
        AdditionalAttributes ??= new Dictionary<string, object>();

        fieldIdentifier = FieldIdentifier.Create(ValueExpression);

        try
        {
            cultureInfo = new CultureInfo(Locale);
        }
        catch (CultureNotFoundException)
        {
            cultureInfo = new CultureInfo("en-US");
        }

        await base.OnInitializedAsync();
    }

    /// <summary>
    /// Disables number input.
    /// </summary>
    public void Disable() => Disabled = true;

    /// <summary>
    /// Enables number input.
    /// </summary>
    public void Enable() => Disabled = false;

    private async Task OnChange(ChangeEventArgs e)
    {
        var oldValue = Value;
        var newValue = e.Value; // object

        if (newValue is null)
            Value = default!;
        else
            Value = newValue?.ToString() ?? "";

        await ValueChanged.InvokeAsync(Value);

        EditContext?.NotifyFieldChanged(fieldIdentifier);
    }

    #endregion

    #region Properties, Indexers

    protected override string? ClassNames =>
        BuildClassNames(Class,
            (BootstrapClass.FormControl, true),
            (TextAlignment.ToTextAlignmentClass(), TextAlignment != Alignment.None));

    /// <summary>
    /// If <see langword="true" />, allows negative numbers.
    /// </summary>
    /// <remarks>
    /// Default value is false.
    /// </remarks>
    [Parameter]
    public bool AllowNegativeNumbers { get; set; }

    private string autoComplete => AutoComplete ? "true" : "false";

    /// <summary>
    /// If <see langword="true" />, NumberInput can complete the values automatically by the browser.
    /// </summary>
    /// <remarks>
    /// Default value is false.
    /// </remarks>
    [Parameter]
    public bool AutoComplete { get; set; }

    /// <summary>
    /// Gets or sets the disabled state .
    /// </summary>
    /// <remarks>
    /// Default value is false.
    /// </remarks>
    [Parameter]
    public bool Disabled { get; set; }

    [CascadingParameter] private EditContext EditContext { get; set; } = default!;

    private string fieldCssClasses => EditContext?.FieldCssClass(fieldIdentifier) ?? "";

    /// <summary>
    /// Gets or sets the locale. Default locale is 'en-US'.
    /// </summary>
    /// <remarks>
    /// Default value is 'en-US'.
    /// </remarks>
    [Parameter]
    //[EditorRequired]
    public string Locale { get; set; } = "en-US";

    /// <summary>
    /// Gets or sets the placeholder.
    /// </summary>
    /// <remarks>
    /// Default value is null.
    /// </remarks>
    [Parameter]
    public string? Placeholder { get; set; }

    /// <summary>
    /// Gets or sets the text alignment.
    /// </summary>
    /// <remarks>
    /// Default value is <see cref="Alignment.None" />.
    /// </remarks>
    [Parameter]
    public Alignment TextAlignment { get; set; } = Alignment.None;

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [Parameter]
    public string Value { get; set; } = default!;

    /// <summary>
    /// This event fired on every user keystroke that changes the TextInput value.
    /// </summary>
    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    [Parameter] public Expression<Func<string>> ValueExpression { get; set; } = default!;

    #endregion
}
