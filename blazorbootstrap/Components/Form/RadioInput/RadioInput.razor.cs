namespace BlazorBootstrap;

public partial class RadioInput<TValue> : BlazorBootstrapComponentBase
{
    #region Fields and Constants

    private CultureInfo cultureInfo = default!;

    private FieldIdentifier fieldIdentifier;

    #endregion

    protected override async Task OnInitializedAsync()
    {
        AdditionalAttributes ??= new Dictionary<string, object>();

        try
        {
            cultureInfo = new CultureInfo(Locale);
        }
        catch (CultureNotFoundException)
        {
            cultureInfo = new CultureInfo("en-US");
        }

        fieldIdentifier = FieldIdentifier.Create(ValueExpression);

        await base.OnInitializedAsync();
    }

    private async Task OnChange(ChangeEventArgs e)
    {
        await OnChangeCallback.InvokeAsync(e.Value?.ToString());
        EditContext?.NotifyFieldChanged(fieldIdentifier);
    }


    #region Properties, Indexers

    protected override string? ClassNames =>
        BuildClassNames(Class,
            (BootstrapClass.FormCheck, true));

    /// <summary>
    /// Disables number input.
    /// </summary>
    public void Disable() => Disabled = true;

    /// <summary>
    /// Enables number input.
    /// </summary>
    public void Enable() => Disabled = false;

    [CascadingParameter] private EditContext EditContext { get; set; } = default!;

    private string fieldCssClasses => EditContext?.FieldCssClass(fieldIdentifier) ?? "";

    /// <summary>
    /// Gets or sets the locale. Default locale is 'en-US'.
    /// </summary>
    /// <remarks>
    /// Default value is 'en-US'.
    /// </remarks>
    [Parameter]
    public string Locale { get; set; } = "en-US";

    /// <summary>
    /// Gets or sets the disabled state .
    /// </summary>
    /// <remarks>
    /// Default value is false.
    /// </remarks>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the control name .
    /// </summary>
    /// <remarks>
    /// Default value is empty string.
    /// </remarks>
    [Parameter]
    public string Name { get; set; } = default;

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [Parameter]
    public string Value { get; set; } = default!;

    /// <summary>
    /// Gets or sets the Label
    /// </summary>
    [Parameter]
    public string Label { get; set; } = default;

    /// <summary>
    /// Gets or sets the HelpText
    /// </summary>
    [Parameter]
    public string HelpText { get; set; } = default;

    [Parameter]
    public bool Checked { get; set; } = default;

    /// <summary>
    /// This event fired on every user click the radio button.
    /// </summary>
    [Parameter]
    public EventCallback OnChangeCallback { get; set; }

    /// <summary>
    /// Gets or sets the expression.
    /// </summary>
    [Parameter]
    public Expression<Func<TValue>> ValueExpression { get; set; } = default!;

    #endregion
}
