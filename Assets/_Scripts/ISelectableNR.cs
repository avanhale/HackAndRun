public interface ISelectableNR
{
    bool CanHighlight();
    bool CanSelect();

    void Highlighted();
    void Selected();
}
