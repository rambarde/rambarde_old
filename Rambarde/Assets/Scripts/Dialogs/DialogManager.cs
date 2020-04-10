using System.Collections;
using System.Collections.Generic;
using Skills;
using Status;
using UI;
using UnityEngine;

public enum CharacterType
{
    None,
    Bard,
    Client,
    Goblins,
    Orcs,
    OrcsLeader,
    Treant,
    Golem,
    Ghost,
    Wight,//Âme en peine
    Wisp,
    Brasier,
    Skeleton,
    AngrySkeleton,
    ColdSkeleton
}

public struct Quote
{
    public CharacterType character;
    public string phrase;
}

public class DialogManager : MonoBehaviour
{
    public float dialogAppearRate = .4f;

    private Dictionary<CharacterType, List<DialogPhrase>> dialogs;
    private List<string> closedList;
    
    public async void Init(List<CharacterType> characters)
    {
        foreach (var character in characters)
        {
            Dialog dialog = await Utils.LoadResource<Dialog>("ScriptableObjects/" + character.ToString());
            dialogs.Add(character, dialog.GetPhrases());
        }
    }
    
    bool IsDialogAvailable()
    {
        return Random.Range(0, 1) <= dialogAppearRate;
    }
    
    public Quote GetDialogQuote(DialogFilter filter, CharacterType actionCharacter1, CharacterType actionCharacter2)
    {
        Quote quote = new Quote() {character = CharacterType.None, phrase = ""};
        if (!IsDialogAvailable()) return quote;

        List<Quote> quotes = new List<Quote>();
        
        foreach (var filteredQuote in GetFilteredCharacterQuotes(filter,CharacterType.Bard))
            quotes.Add(filteredQuote);

        if(actionCharacter1 != CharacterType.None)
            foreach (var filteredQuote in GetFilteredCharacterQuotes(filter,actionCharacter1))
                quotes.Add(filteredQuote);
        
        if(actionCharacter2 != CharacterType.None)
            foreach (var filteredQuote in GetFilteredCharacterQuotes(filter,actionCharacter2))
                quotes.Add(filteredQuote);

        if (quotes.Count != 0)
        {
            int rand = Random.Range(0, quotes.Count);
            closedList.Add(quotes[rand].phrase);
            quote.character = quotes[rand].character;
            quote.phrase = quotes[rand].phrase;
        }

        return quote;
    }

    private IEnumerable<Quote> GetFilteredCharacterQuotes(DialogFilter filter, CharacterType character)
    {
        foreach (var dialogPhrase in dialogs[character])
        {
            if ((filter & DialogFilter.Buff) == DialogFilter.Buff) // if filter contains a buff
            {
                if ((dialogPhrase.filter | DialogFilter.Buff) == DialogFilter.Buff) // only buff
                {
                    if (!closedList.Contains(dialogPhrase.phrase))
                        yield return new Quote() {character = character, phrase = dialogPhrase.phrase};
                }
                else if ((dialogPhrase.filter & filter) == filter) // buff with type
                {
                    if (!closedList.Contains(dialogPhrase.phrase))
                        yield return new Quote() {character = character, phrase = dialogPhrase.phrase};
                }
            }
            else if ((filter & DialogFilter.Unbuff) == DialogFilter.Unbuff) // if filter contains a unbuff
            {
                if ((dialogPhrase.filter | DialogFilter.Unbuff) == DialogFilter.Unbuff) // only unbuff
                {
                    if (!closedList.Contains(dialogPhrase.phrase))
                        yield return new Quote() {character = character, phrase = dialogPhrase.phrase};
                }
                else if ((dialogPhrase.filter & filter) == filter) // unbuff with type
                {
                    if (!closedList.Contains(dialogPhrase.phrase))
                        yield return new Quote() {character = character, phrase = dialogPhrase.phrase};
                }
                
                //to do handle client -> monster unbuff and fakemonster -> client cases
            }
            else if ((dialogPhrase.filter & filter) == filter)
            {
                if (!closedList.Contains(dialogPhrase.phrase))
                    yield return new Quote() {character = character, phrase = dialogPhrase.phrase};
            }
        }
            
                
    }
}
