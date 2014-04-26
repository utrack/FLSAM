using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FLAccountDB.Data;
using FLSAM.Forms;
using FLSAM.GD;
using LogDispatcher;

namespace FLSAM.AccountHelper
{
    public static class EquipTable
    {
        public static uiTables.ShipEquipDataTable GetTable(Character curCharacter,LogDispatcher.LogDispatcher log)
        {

            var eqList = new uiTables.ShipEquipDataTable();

            var shipHP = Universe.Gis.Ships.FindByHash(curCharacter.ShipArch);
            if (shipHP != null)
                foreach (var hp in shipHP.GetHardpointsRows())
                {
                    eqList.AddShipEquipRow(hp.Name, hp.EquipType, "",hp.HPType);
                }


            foreach (var equip in curCharacter.EquipmentList)
            {
                var eq = Universe.Gis.Equipment.FindByHash(equip.Item1);
                string eqNick;
                var eqType = "";
                if (eq == null)
                {
                    eqNick = equip.Item1.ToString(CultureInfo.InvariantCulture);
                    if (!Universe.IsAttached)
                        log.NewMessage(LogType.Warning, "Equip {0} not found while reading character {1}!", equip.Item1,
                            curCharacter.Name);
                }
                else
                {
                    eqNick = eq.Nickname;
                    eqType = eq.Type;
                }
                    
                
                //ID,HP,health

                //internal HP
                if (equip.Item2 == "")
                {
                    eqList.AddShipEquipRow("", eqType, eqNick, "");
                    continue;
                }

                var firstOrDefault = eqList.FirstOrDefault(w => w.HPName == equip.Item2);
                if (firstOrDefault != null)
                    firstOrDefault[2] = eqNick;
                else
                {
                    eqList.AddShipEquipRow(equip.Item2, eqType, eqNick, "");
                }
            }

            return eqList;
        }

        /// <summary>
        /// Generate new EquipmentList based on ShipEquipDataTable
        /// </summary>
        /// <param name="eqTable">Source EquipTable.</param>
        /// <returns>List of equipment; can be assigned to Character's EquipmentList.</returns>
        public static List<Tuple<uint, string, float>> GetEquipmentList(uiTables.ShipEquipDataTable eqTable)
        {
            return (from uiTables.ShipEquipRow row in eqTable.Rows 
                    where row.Equipment != "" 
                    select 
                    new Tuple<uint, string, float>(Universe.CreateID(row.Equipment), row.HPName, 1f))
                    .ToList();
        }

        /// <summary>
        /// Returns cargo list for those archived accounts.
        /// </summary>
        /// <param name="equip"></param>
        /// <returns></returns>
        public static List<WTuple<uint, uint>> GetTableFallback(string equip)
        {
            var ret = new List<WTuple<uint, uint>>();

            foreach (var eqItem in equip.Split(' '))
            {
                var equipHash = uint.Parse(eqItem);
                var prevItem = ret.FirstOrDefault(w => w.Item1 == equipHash);
                if (prevItem == null)
                    ret.Add(new WTuple<uint, uint>(equipHash, 1));
                else
                    prevItem.Item2++;
            }
            return ret;

        }
    }
}
