using System;
using System.Globalization;
using System.Linq;
using FLAccountDB;
using FLSAM.Forms;
using FLSAM.GD;
using LogDispatcher;

namespace FLSAM.AccountHelper
{
    public static class EquipTable
    {
        public static uiTables.ShipEquipDataTable GetTable(Character curCharacter)
        {

            var eqList = new uiTables.ShipEquipDataTable();

            foreach (var hp in Universe.Gis.Ships.FindByHash(curCharacter.ShipArch).GetHardpointsRows())
            {
                eqList.AddShipEquipRow(hp.Name, hp.EquipType, "",hp.HPType);
            }


            foreach (var equip in curCharacter.EquipmentList)
            {
                if (equip.Item2 == "")
                {
                    var eq = Universe.Gis.Equipment.FindByHash(equip.Item1);
                    if (eq != null)
                        eqList.AddShipEquipRow(equip.Item2, eq.Type, eq.Nickname,eq.Hardpoint);
                    else
                        eqList.AddShipEquipRow(equip.Item2, "", equip.Item1.ToString(CultureInfo.InvariantCulture),"");

                    //eqList.AddShipEquipRow("", "Internal", _uni.Gis.Equipment.FindByHash(equip.Item1).Nickname);
                    continue;
                }



                var firstOrDefault = eqList.FirstOrDefault(w => w.HPName == equip.Item2);
                if (firstOrDefault != null)
                    firstOrDefault[2] =
                        Universe.Gis.Equipment.FindByHash(equip.Item1).Nickname;
                else
                {
                    var eq = Universe.Gis.Equipment.FindByHash(equip.Item1);
                    if (eq != null)
                        eqList.AddShipEquipRow(equip.Item2, eq.Type, eq.Nickname,eq.Hardpoint);
                    else
                        eqList.AddShipEquipRow(equip.Item2, "Internal", equip.Item1.ToString(CultureInfo.InvariantCulture),"");
                    LogDispatcher.LogDispatcher.NewMessage(LogType.Warning, "Equip {0} not found while reading character {1}!", equip.Item1, curCharacter.Name);
                }
            }

            return eqList;
        }

        public static void RetEquipment(Character ch,uiTables.ShipEquipDataTable eqTable)
        {
            ch.EquipmentList.Clear();

            foreach (uiTables.ShipEquipRow row in eqTable.Rows)
            {
                ch.EquipmentList.Add(
                    new Tuple<uint, string, float>(Universe.CreateID(row.Equipment), row.HPName, 1f)
                    );
            }
        }

    }
}
