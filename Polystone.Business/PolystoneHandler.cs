using Microsoft.EntityFrameworkCore;
using POGOProtos.Rpc;
using Polystone.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Polystone.Business
{
    public static class PolystoneHandler
    {
        private static PolystoneContext _polystoneContext = new PolystoneContext();

        static void HandleCatchPokemon(Account account, Payload payload)
        {
            CatchPokemonOutProto catchPokemonOutProto = CatchPokemonOutProto.Parser.ParseFrom(
                payload.ConvertFromBase64String()
            );
            if (catchPokemonOutProto.Status == CatchPokemonOutProto.Types.Status.CatchSuccess)
            {
                _polystoneContext.AccountCatches.Add(new AccountCatch()
                {
                    Id = catchPokemonOutProto.CapturedPokemonId,
                    Experience = catchPokemonOutProto.Scores.Exp.Sum(),
                    Stardust = catchPokemonOutProto.Scores.Stardust.Sum(),
                    IsShiny = catchPokemonOutProto.PokemonDisplay.Shiny,
                    IsShadow = (catchPokemonOutProto.PokemonDisplay.Alignment == PokemonDisplayProto.Types.Alignment.Shadow),
                    Longitude = payload.Lng,
                    Latitude = payload.Lat,
                    Account = account
                });
                _polystoneContext.SaveChanges();
            }
        }

        //static void HandleGymFeedPokemon(Account account, Payload payload)
        //{
        //    GymFeedPokemonOutProto gymFeedPokemonOut = GymFeedPokemonOutProto.Parser.ParseFrom(
        //        payload.ConvertFromBase64String()
        //    );

        //}

        //static void HandleCompleteQuest(Account account, Payload payload)
        //{
        //    CompleteQuestOutProto completeQuestOut = CompleteQuestOutProto.Parser.ParseFrom(
        //        payload.ConvertFromBase64String()
        //    );

        //}

        //static void HandleCompleteQuestStampCard(Account account, Payload payload)
        //{
        //    CompleteQuestStampCardOutProto completeQuestStampCardOut = CompleteQuestStampCardOutProto.Parser.ParseFrom(
        //        payload.ConvertFromBase64String()
        //    );

        //}

        //static void HandleGetHatchedEggs(Account account, Payload payload)
        //{
        //    GetHatchedEggsOutProto getHatchedEggsOut = GetHatchedEggsOutProto.Parser.ParseFrom(
        //        payload.ConvertFromBase64String()
        //    );

        //}

        //static void HandleFortSearch(Account account, Payload payload)
        //{
        //    FortSearchOutProto fortSearchOut = FortSearchOutProto.Parser.ParseFrom(
        //        payload.ConvertFromBase64String()
        //    );

        //}

        //static void HandleEvolvePokemon(Account account, Payload payload)
        //{
        //    EvolvePokemonOutProto evolvePokemonOut = EvolvePokemonOutProto.Parser.ParseFrom(
        //        payload.ConvertFromBase64String()
        //    );

        //}

        //static void HandleInvasionBattleUpdate(Account account, Payload payload)
        //{
        //    UpdateInvasionBattleOutProto updateInvasionBattleOut = UpdateInvasionBattleOutProto.Parser.ParseFrom(
        //        payload.ConvertFromBase64String()
        //    );

        //}

        //static void HandleAttackRaid(Account account, Payload payload)
        //{
        //    AttackRaidBattleOutProto attackRaidBattleOut = AttackRaidBattleOutProto.Parser.ParseFrom(
        //        payload.ConvertFromBase64String()
        //    );

        //}

        static void HandleGetPlayer(Account account, Payload payload)
        {
            GetPlayerOutProto getPlayerOut = GetPlayerOutProto.Parser.ParseFrom(
                payload.ConvertFromBase64String()
            );
            if (!getPlayerOut.Success)
            {
                return;
            }
            
            int pokeCoin = getPlayerOut.Player.CurrencyBalance.FirstOrDefault(c_ => c_.CurrencyType == "POKECOIN").Quantity;
            int stardust = getPlayerOut.Player.CurrencyBalance.FirstOrDefault(c_ => c_.CurrencyType == "STARDUST").Quantity;

            account.CreationDate = DateTimeOffset.FromUnixTimeMilliseconds(getPlayerOut.Player.CreationTimeMs).DateTime;

            if (pokeCoin != account.CurrentHistory.Pokecoin || (stardust - account.CurrentHistory.Stardust >= 10000))
            {
                AccountHistory accountHistory = new AccountHistory()
                {
                    CreationDate = DateTime.UtcNow,
                    Level = account.CurrentHistory.Level,
                    Experience = account.CurrentHistory.Experience,
                    Pokecoin = pokeCoin,
                    Stardust = stardust,
                    PokemonCaught = account.CurrentHistory.PokemonCaught,
                    PokestopSpinned = account.CurrentHistory.PokestopSpinned,
                    Longitude = payload.Lng,
                    Latitude = payload.Lat
                };
                account.AccountHistories.Add(accountHistory);
                _polystoneContext.SaveChanges();
                account.CurrentHistory = accountHistory;
                account.LastUpdateDate = DateTime.UtcNow;
            }
            _polystoneContext.Update(account);
            _polystoneContext.SaveChanges();
        }

        static void HandleGetHoloholoInventory(Account account, Payload payload)
        {
            GetHoloholoInventoryOutProto getHoloholoInventoryOut = GetHoloholoInventoryOutProto.Parser.ParseFrom(
                payload.ConvertFromBase64String()
            );
            if (!getHoloholoInventoryOut.Success)
            {
                return;
            }

            if(getHoloholoInventoryOut.InventoryDelta?.InventoryItem?.Count == 0)
            {
                return;
            }

            InventoryItemProto playerStatsInventoryItemData = getHoloholoInventoryOut.InventoryDelta?.InventoryItem?.FirstOrDefault(i_ =>
                i_.InventoryItemData?.TypeCase == HoloInventoryItemProto.TypeOneofCase.PlayerStats
            );
            if (playerStatsInventoryItemData != null)
            {
                PlayerStatsProto playerStats = playerStatsInventoryItemData.InventoryItemData.PlayerStats;
                if (playerStats.Experience - account.CurrentHistory.Experience >= 10000)
                {
                    AccountHistory accountHistory = new AccountHistory()
                    {
                        CreationDate = DateTime.UtcNow,
                        Level = playerStats.Level,
                        Experience = playerStats.Experience,
                        Pokecoin = account.CurrentHistory.Pokecoin,
                        Stardust = account.CurrentHistory.Stardust,
                        PokemonCaught = playerStats.NumPokemonCaptured,
                        PokestopSpinned = playerStats.PokeStopVisits,
                        Longitude = payload.Lng,
                        Latitude = payload.Lat
                    };
                    account.AccountHistories.Add(accountHistory);
                    _polystoneContext.SaveChanges();
                    account.CurrentHistory = accountHistory;
                    account.LastUpdateDate = DateTime.UtcNow;
                }
                _polystoneContext.Update(account);
                _polystoneContext.SaveChanges();
            }

            Dictionary<ulong, PokemonProto> pokemonInventoryItemData = getHoloholoInventoryOut.InventoryDelta?.InventoryItem.Where(i_ =>
                i_.InventoryItemData?.TypeCase == HoloInventoryItemProto.TypeOneofCase.Pokemon
            ).Select(i_ => i_.InventoryItemData.Pokemon).ToDictionary(p_ => p_.Id, p_ => p_);
            foreach (AccountCatch pokemon in _polystoneContext.AccountCatches.Where(p_ => pokemonInventoryItemData.Select(pp_ => pp_.Key).Contains(p_.Id)))
            {
                PokemonProto pokemonProto = pokemonInventoryItemData[pokemon.Id];
                pokemon.Specie = (int)pokemonProto.PokemonId;
                pokemon.Cp = pokemonProto.Cp;
                pokemon.CatchDate = DateTimeOffset.FromUnixTimeMilliseconds(pokemonProto.CreationTimeMs).DateTime;
                _polystoneContext.AccountCatches.Update(pokemon);
            }
            _polystoneContext.SaveChanges();

            foreach(PokemonFamilyProto pokemonFamily in getHoloholoInventoryOut.InventoryDelta?.InventoryItem.Where(i_ => i_.InventoryItemData?.TypeCase == HoloInventoryItemProto.TypeOneofCase.PokemonFamily).Select(i_ => i_.InventoryItemData.PokemonFamily))
            {
                AccountCandy candy = _polystoneContext.AccountCandies.FirstOrDefault(c_ => c_.Specie == (int)pokemonFamily.FamilyId);
                if(candy == null)
                {
                    AccountCandy newCandy = new AccountCandy()
                    {
                        Specie = (int)pokemonFamily.FamilyId,
                        SmallCandy = pokemonFamily.Candy,
                        XLCandy = pokemonFamily.XlCandy,
                        Account = account
                    };
                    _polystoneContext.AccountCandies.Add(newCandy);
                } 
                else
                {
                    candy.SmallCandy = pokemonFamily.Candy;
                    candy.XLCandy = pokemonFamily.XlCandy;
                    _polystoneContext.AccountCandies.Update(candy);
                }
                _polystoneContext.SaveChanges();
            }
        }

        public static void Handle(Payload payload)
        {
            Account account = _polystoneContext.Accounts.Include(a_ => a_.CurrentHistory).FirstOrDefault(a_ => 
                a_.Name == payload.AccountName
            );
            if (account == null)
            {
                Account accountCreation = new Account()
                {
                    Name = payload.AccountName,
                    LastUpdateDate = DateTime.UtcNow
                };
                _polystoneContext.Accounts.Add(accountCreation);
                _polystoneContext.SaveChanges();

                AccountHistory accountHistory = new AccountHistory()
                {
                    CreationDate = DateTime.UtcNow,
                    Level = Convert.ToInt32(payload.Level),
                    Longitude = payload.Lng,
                    Latitude = payload.Lat,
                    Account = accountCreation
                };
                accountCreation.CurrentHistory = accountHistory;
                _polystoneContext.SaveChanges();
                account = accountCreation;
            }

            switch (payload.GetMethod())
            {
                case Method.CatchPokemon:
                    HandleCatchPokemon(account, payload);
                    break;
                //case Method.GymFeedPokemon:
                //    HandleGymFeedPokemon(account, payload);
                //    break;
                //case Method.CompleteQuest:
                //    HandleCompleteQuest(account, payload);
                //    break;
                //case Method.CompleteQuestStampCard:
                //    HandleCompleteQuestStampCard(account, payload);
                //    break;
                //case Method.GetHatchedEggs:
                //    HandleGetHatchedEggs(account, payload);
                //    break;
                //case Method.FortSearch:
                //    HandleFortSearch(account, payload);
                //    break;
                //case Method.EvolvePokemon:
                //    HandleEvolvePokemon(account, payload);
                //    break;
                //case Method.InvasionBattleUpdate:
                //    HandleInvasionBattleUpdate(account, payload);
                //    break;
                //case Method.AttackRaid:
                //    HandleAttackRaid(account, payload);
                //    break;
                case Method.GetPlayer:
                    HandleGetPlayer(account, payload);
                    break;
                case Method.GetHoloholoInventory:
                    HandleGetHoloholoInventory(account, payload);
                    break;
                default:
                    break;
            }
        }
    }
}
