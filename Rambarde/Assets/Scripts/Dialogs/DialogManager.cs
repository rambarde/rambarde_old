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
    
    public Quote GetDialogQuote(DialogFilter filter, CharacterType playerTeamCharacter, CharacterType enemyTeamCharacter, SkillAction skillAction)
    {
        Quote quote = new Quote() {character = CharacterType.None, phrase = ""};
        if (!IsDialogAvailable()) return quote;

        List<Quote> quotes = new List<Quote>();
        
        foreach (var dialogPhrase in dialogs[CharacterType.Bard])
            if ((dialogPhrase.filter & filter) == filter)
                if(!closedList.Contains(dialogPhrase.phrase))
                    quotes.Add(new Quote() {character = CharacterType.Bard, phrase = dialogPhrase.phrase});

        if(playerTeamCharacter != CharacterType.None)
            foreach (var dialogPhrase in dialogs[playerTeamCharacter])
                if ((dialogPhrase.filter & filter) == filter)
                    if(!closedList.Contains(dialogPhrase.phrase))
                        quotes.Add(new Quote() {character = playerTeamCharacter, phrase = dialogPhrase.phrase});
        
        if(enemyTeamCharacter != CharacterType.None)
            foreach (var dialogPhrase in dialogs[playerTeamCharacter])
                if ((dialogPhrase.filter & filter) == filter)
                    if(!closedList.Contains(dialogPhrase.phrase))
                        quotes.Add(new Quote() {character = enemyTeamCharacter, phrase = dialogPhrase.phrase});

        if (quotes.Count != 0)
        {
            int rand = Random.Range(0, quotes.Count);
            closedList.Add(quotes[rand].phrase);
            quote.character = quotes[rand].character;
            quote.phrase = quotes[rand].phrase;
        }

        return quote;
    }
}
