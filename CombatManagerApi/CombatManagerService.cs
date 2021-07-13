using System.Diagnostics;
using System.Threading.Tasks;
using CombatManager.Api.Core.Data;
using CombatManager.Api.Core.Request;
using CombatManager.Api.Unused;

namespace CombatManager.Api
{
    public interface ICombatManagerService
    {
        Task<RemoteCombatState> GetCombatState();
        Task<RemoteCombatState> CombatNext();
        Task<RemoteCombatState> CombatPrev();
        Task<RemoteCombatState> CombatRollInit();
        Task<RemoteCharacter> GetCharacterDetails(string charid);
        Task<RemoteCombatState> MoveCharacterUp(string charid);
        Task<RemoteCombatState> MoveDownCharacter(string charid);
        Task<RemoteCombatState> DeleteCharacter(string charid);
        Task<RemoteCombatState> ReadyCharacter(string charid);
        Task<RemoteCombatState> UnreadyCharacter(string charid);
        Task<RemoteCombatState> DelayCharacter(string charid);
        Task<RemoteCombatState> UndelayCharacter(string charid);
        Task<RemoteCombatState> CharacterActNow(string charid);
        Task<RemoteCombatState> ChangeHP(string charid, int amount);
        Task<RemoteCharacter> ChangeMaxHP(string charid, int amount);
        Task<RemoteCharacter> ChangeTemporaryHP(string charid, int amount);
        Task<RemoteCharacter> ChangeNonlethalDamage(string charid, int amount);
        Task<RemoteCharacter> HideCharacter(string charid, bool state);
        Task<RemoteCharacter> IdleCharacter(string charid, bool state);
        Task<RemoteCharacter> AddCondition(AddConditionRequest request);
        Task<RemoteCharacter> RemoveCondition(RemoveConditionRequest request);
        Task<RemoteDBListing> ListMonsters(MonsterListRequest request);
        Task<RemoteMonster> GetMonster(MonsterRequest request);
        Task<RemoteFeat> GetFeat(FeatRequest request);
        Task<RemoteFeatList> GetFeats(FeatsRequest request);
        Task<RemoteDBListing> ListFeats(FeatListRequest request);
        Task<RemoteSpell> GetSpell(SpellRequest request);
        Task<RemoteSpellList> GetSpells(SpellsRequest request);
        Task<RemoteDBListing> ListSpells(SpellListRequest request);
        Task<RemoteMagicItem> GetMagicItem(MagicItemRequest request);
        Task<RemoteMagicItemList> GetMagicItems(MagicItemsRequest request);
        Task<RemoteDBListing> ListMagicItems(MagicItemListRequest request);
        Task<RemoteMonster> GetRegularMonster(int id);
        Task<string> BringToFront();
        Task<string> Minimize();
        Task<string> UIGoto(string place);
        Task<string> ShowCombatList();
        Task<string> HideCombatList();
        Task<string> GetCustomMonster(int id);
        Task<RemoteMonsterList> GetMonsters(MonstersRequest request);
        Task<RemoteMonster> AddMonster(MonsterAddRequest request);
    }

    public class CombatManagerService : ICombatManagerService
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
        public async Task<RemoteCombatState> GetCombatState() => await Route<RemoteCombatState>(HttpVerbs.Get, "/combat/state");
        public async Task<RemoteCombatState> CombatNext() => await Route<RemoteCombatState>(HttpVerbs.Get, "/combat/next");
        public async Task<RemoteCombatState> CombatPrev() => await Route<RemoteCombatState>(HttpVerbs.Get, "/combat/prev");
        public async Task<RemoteCombatState> CombatRollInit() => await Route<RemoteCombatState>(HttpVerbs.Get, "/combat/rollinit");
        public async Task<RemoteCharacter> GetCharacterDetails(string charid) => await Route<RemoteCharacter>(HttpVerbs.Get, $"/character/details/{charid}");
        public async Task<RemoteCombatState> MoveCharacterUp(string charid) => await Route<RemoteCombatState>(HttpVerbs.Get, $"/combat/moveupcharacter/{charid}");
        public async Task<RemoteCombatState> MoveDownCharacter(string charid) => await Route<RemoteCombatState>(HttpVerbs.Get, $"/combat/movedowncharacter/{charid}");
        public async Task<RemoteCombatState> DeleteCharacter(string charid) => await Route<RemoteCombatState>(HttpVerbs.Get, $"/combat/deletecharacter/{charid}");
        public async Task<RemoteCombatState> ReadyCharacter(string charid) => await Route<RemoteCombatState>(HttpVerbs.Get, $"/combat/ready/{charid}");
        public async Task<RemoteCombatState> UnreadyCharacter(string charid) => await Route<RemoteCombatState>(HttpVerbs.Get, $"/combat/unready/{charid}");
        public async Task<RemoteCombatState> DelayCharacter(string charid) => await Route<RemoteCombatState>(HttpVerbs.Get, $"/combat/delay/{charid}");
        public async Task<RemoteCombatState> UndelayCharacter(string charid) => await Route<RemoteCombatState>(HttpVerbs.Get, $"/combat/undelay/{charid}");
        public async Task<RemoteCombatState> CharacterActNow(string charid) => await Route<RemoteCombatState>(HttpVerbs.Get, $"/combat/actnow/{charid}");
        public async Task<RemoteCombatState> ChangeHP(string charid, int amount) => await Route<RemoteCombatState>(HttpVerbs.Get, $"/character/changehp/{charid}/{amount}");

        public async Task<RemoteCharacter> ChangeMaxHP(string charid, int amount) => await Route<RemoteCharacter>(HttpVerbs.Get, $"/character/changemaxhp/{charid}/{amount}");
        public async Task<RemoteCharacter> ChangeTemporaryHP(string charid, int amount) => await Route<RemoteCharacter>(HttpVerbs.Get, $"/character/changetemporaryhp/{charid}/{amount}");
        public async Task<RemoteCharacter> ChangeNonlethalDamage(string charid, int amount) => await Route<RemoteCharacter>(HttpVerbs.Get, $"/character/changenonlethaldamage/{charid}/{amount}");
        public async Task<RemoteCharacter> HideCharacter(string charid, bool state) => await Route<RemoteCharacter>(HttpVerbs.Get, $"/character/hide/{charid}/{state}");
        public async Task<RemoteCharacter> IdleCharacter(string charid, bool state) => await Route<RemoteCharacter>(HttpVerbs.Get, $"/character/idle/{charid}/{state}");
        public async Task<RemoteCharacter> AddCondition(AddConditionRequest request) => await RoutePost<RemoteCharacter>("/character/addcondition", request);
        public async Task<RemoteCharacter> RemoveCondition(RemoveConditionRequest request) => await RoutePost<RemoteCharacter>("/character/removecondition", request);
        public async Task<RemoteDBListing> ListMonsters(MonsterListRequest request) => await RoutePost<RemoteDBListing>("/monster/list", request);
        public async Task<RemoteMonster> GetMonster(MonsterRequest request) => await RoutePost<RemoteMonster>("/monster/get", request);
        public async Task<RemoteFeat> GetFeat(FeatRequest request) => await RoutePost<RemoteFeat>("/feat/get", request);
        public async Task<RemoteFeatList> GetFeats(FeatsRequest request) => await RoutePost<RemoteFeatList>("/feat/fromlist", request);
        public async Task<RemoteDBListing> ListFeats(FeatListRequest request) => await RoutePost<RemoteDBListing>("/feat/list", request);
        public async Task<RemoteSpell> GetSpell(SpellRequest request) => await RoutePost<RemoteSpell>("/spell/get", request);
        public async Task<RemoteSpellList> GetSpells(SpellsRequest request) => await RoutePost<RemoteSpellList>("/spell/fromlist", request);
        public async Task<RemoteDBListing> ListSpells(SpellListRequest request) => await RoutePost<RemoteDBListing>("/spell/list", request);
        public async Task<RemoteMagicItem> GetMagicItem(MagicItemRequest request) => await RoutePost<RemoteMagicItem>("/magicitem/get", request);

        public async Task<RemoteMagicItemList> GetMagicItems(MagicItemsRequest request) => await RoutePost<RemoteMagicItemList>("/magicitem/fromlist", request);
        public async Task<RemoteDBListing> ListMagicItems(MagicItemListRequest request) => await RoutePost<RemoteDBListing>("/magicitem/list", request);
        public async Task<RemoteMonster> GetRegularMonster(int id) => await Route<RemoteMonster>(HttpVerbs.Get, $"/monster/getregular/{id}");
        public async Task<string> BringToFront() => await Route(HttpVerbs.Get, "/ui/bringtofront");
        public async Task<string> Minimize() => await Route(HttpVerbs.Get, "/ui/minimize");


        public async Task<string> UIGoto(string place) => await Route(HttpVerbs.Get, $"/ui/goto/{place}");
        public async Task<string> ShowCombatList() => await Route(HttpVerbs.Get, "/ui/showcombatlist");

        public async Task<string> HideCombatList() => await Route(HttpVerbs.Get, "/ui/hidecombatlist");
        public async Task<string> GetCustomMonster(int id) => await Route(HttpVerbs.Get, $"/monster/getcustom/{id}");
        public async Task<RemoteMonsterList> GetMonsters(MonstersRequest request) => await RoutePost<RemoteMonsterList>("/monster/fromlist", request);
        public async Task<RemoteMonster> AddMonster(MonsterAddRequest request) => await RoutePost<RemoteMonster>("/monster/add", request);
    }
}
