using CardActionService.Domain.Enums;

namespace CardActionService.Domain.Mappers;

public static class ActionFlagMapper
{
    public static readonly IReadOnlyDictionary<char, EnActionFlag> SymbolMap = new Dictionary<char, EnActionFlag>
    {
        { 'N', EnActionFlag.No },
        { 'Y', EnActionFlag.Yes },
        { 'U', EnActionFlag.UnlessNoPin },
        { 'P', EnActionFlag.IfPinSet },
        { 'Q', EnActionFlag.IfPinNotSet },
    };
}