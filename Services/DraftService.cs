using Microsoft.EntityFrameworkCore;
using magick.Models;

namespace magick.Services;

public class DraftService
{
    public void StartDraft(int setId)
        => throw new NotImplementedException();

    public void FinishDraft()
        => throw new NotImplementedException();

    public bool IsDrafting()
        => throw new NotImplementedException();


    public List<Card> OpenPack()
        => throw new NotImplementedException();


    public void AddCardToDeck(int cardTableIndex)
        => throw new NotImplementedException();

    public void RemoveCardFromDeck(int cardDeckIndex)
        => throw new NotImplementedException();


    public List<Card> GetTable()
        => throw new NotImplementedException();

    public List<Card> GetDeck()
        => throw new NotImplementedException();
}
