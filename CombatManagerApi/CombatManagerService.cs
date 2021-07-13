using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CombatManagerApi.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace CombatManagerApi
{
    public class CombatManagerService
    {
        public CombatManagerService(string address)
        {
            RootAddress = address;
            
        }
        public CombatManagerService()
        {
        }
        public string RootAddress { get; set; }
        public string Passcode { get; set; }
        public string BaseAddress(string partialAddress) => RootAddress + "/api" + partialAddress;
        private async Task<T> Route<T>(RouteAttribute attribute)
        {
            return await NetworkingExtensions.GetAsync<T>(BaseAddress(attribute.Address),Passcode);
        }


        private Task<T> RoutePost<T>(string address, object request)
        {
            return NetworkingExtensions.PostAsync<T>(BaseAddress(address), request);
        }

        private async Task<T> Route<T>(HttpVerbs verb, string partialAddress)
        {
            var fullAddress = BaseAddress(partialAddress);
            Debug.WriteLine(fullAddress);
            return await NetworkingExtensions.GetAsync<T>(fullAddress, Passcode);
        }
        private async Task<string> Route(HttpVerbs verb, string partialAddress)
        {
            var fullAddress = BaseAddress(partialAddress);
            Debug.WriteLine(fullAddress);
            return await NetworkingExtensions.GetAsync(fullAddress, Passcode);
        }
        public async Task<CombatState> GetCombatState() => await Route<CombatState>(HttpVerbs.Get, "/combat/state");
        public async Task<CombatState> CombatNext() => await Route<CombatState>(HttpVerbs.Get, "/combat/next");
        public async Task<CombatState> CombatPrev() => await Route<CombatState>(HttpVerbs.Get, "/combat/prev");
        public async Task<CombatState> CombatRollInit() => await Route<CombatState>(HttpVerbs.Get, "/combat/rollinit");
        public async Task<Character> GetCharacterDetails(string charid) => await Route<Character>(HttpVerbs.Get, $"/character/details/{charid}");
        public async Task<CombatState> MoveCharacterUp(string charid) => await Route<CombatState>(HttpVerbs.Get, $"/combat/moveupcharacter/{charid}");
        public async Task<CombatState> MoveDownCharacter(string charid) => await Route<CombatState>(HttpVerbs.Get, $"/combat/movedowncharacter/{charid}");
        public async Task<CombatState> DeleteCharacter(string charid) => await Route<CombatState>(HttpVerbs.Get, $"/combat/deletecharacter/{charid}");
        public async Task<CombatState> ReadyCharacter(string charid) => await Route<CombatState>(HttpVerbs.Get, $"/combat/ready/{charid}");
        public async Task<CombatState> UnreadyCharacter(string charid) => await Route<CombatState>(HttpVerbs.Get, $"/combat/unready/{charid}");
        public async Task<CombatState> DelayCharacter(string charid) => await Route<CombatState>(HttpVerbs.Get, $"/combat/delay/{charid}");
        public async Task<CombatState> UndelayCharacter(string charid) => await Route<CombatState>(HttpVerbs.Get, $"/combat/undelay/{charid}");
        public async Task<CombatState> CharacterActNow(string charid) => await Route<CombatState>(HttpVerbs.Get, $"/combat/actnow/{charid}");
        public async Task<CombatState> ChangeHP(string charid, int amount) => await Route<CombatState>(HttpVerbs.Get, $"/character/changehp/{charid}/{amount}");

        public async Task<Character> ChangeMaxHP(string charid, int amount) => await Route<Character>(HttpVerbs.Get, $"/character/changemaxhp/{charid}/{amount}");
        public async Task<Character> ChangeTemporaryHP(string charid, int amount) => await Route<Character>(HttpVerbs.Get, $"/character/changetemporaryhp/{charid}/{amount}");
        public async Task<Character> ChangeNonlethalDamage(string charid, int amount) => await Route<Character>(HttpVerbs.Get, $"/character/changenonlethaldamage/{charid}/{amount}");
        public async Task<Character> HideCharacter(string charid, bool state) => await Route<Character>(HttpVerbs.Get, $"/character/hide/{charid}/{state}");
        public async Task<Character> IdleCharacter(string charid, bool state) => await Route<Character>(HttpVerbs.Get, $"/character/idle/{charid}/{state}");
        public async Task<Character> AddCondition(AddConditionRequest request) => await RoutePost<Character>("/character/addcondition", request);
        public async Task<Character> RemoveCondition(RemoveConditionRequest request) => await RoutePost<Character>("/character/removecondition", request);
        public async Task<DBListing> ListMonsters(MonsterListRequest request) => await RoutePost<DBListing>("/monster/list", request);
        public async Task<Monster> GetMonster(MonsterRequest request) => await RoutePost<Monster>("/monster/get", request);
        public async Task<Feat> GetFeat(FeatRequest request) => await RoutePost<Feat>("/feat/get", request);
        public async Task<FeatList> GetFeats(FeatsRequest request) => await RoutePost<FeatList>("/feat/fromlist", request);
        public async Task<DBListing> ListFeats(FeatListRequest request) => await RoutePost<DBListing>("/feat/list", request);
        public async Task<Spell> GetSpell(SpellRequest request) => await RoutePost<Spell>("/spell/get", request);
        public async Task<SpellList> GetSpells(SpellsRequest request) => await RoutePost<SpellList>("/spell/fromlist", request);
        public async Task<DBListing> ListSpells(SpellListRequest request) => await RoutePost<DBListing>("/spell/list", request);
        public async Task<MagicItem> GetMagicItem(MagicItemRequest request) => await RoutePost<MagicItem>("/magicitem/get", request);

        public async Task<MagicItemList> GetMagicItems(MagicItemsRequest request) => await RoutePost<MagicItemList>("/magicitem/fromlist", request);
        public async Task<DBListing> ListMagicItems(MagicItemListRequest request) => await RoutePost<DBListing>("/magicitem/list", request);
        public async Task<Monster> GetRegularMonster(int id) => await Route<Monster>(HttpVerbs.Get, $"/monster/getregular/{id}");
        public async Task<string> BringToFront() => await Route(HttpVerbs.Get, "/ui/bringtofront");
        public async Task<string> Minimize() => await Route(HttpVerbs.Get, "/ui/minimize");


        public async Task<string> UIGoto(string place) => await Route(HttpVerbs.Get, $"/ui/goto/{place}");
        public async Task<string> ShowCombatList() => await Route(HttpVerbs.Get, "/ui/showcombatlist");

        public async Task<string> HideCombatList() => await Route(HttpVerbs.Get, "/ui/hidecombatlist");
        public async Task<string> GetCustomMonster(int id) => await Route(HttpVerbs.Get, $"/monster/getcustom/{id}");
        public async Task<MonsterList> GetMonsters(MonstersRequest request) => await RoutePost<MonsterList>("/monster/fromlist", request);
        public async Task<Monster> AddMonster(MonsterAddRequest request) => await RoutePost<Monster>("/monster/add", request);
    }
}
