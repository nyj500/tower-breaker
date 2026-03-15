using TowerBreaker.Data;

namespace TowerBreaker.Equipment
{
    public class OwnedItem
    {
        public readonly ItemDataSO data;
        public readonly int uid;

        private static int _next = 0;

        public OwnedItem(ItemDataSO data)
        {
            this.data = data;
            uid = _next++;
        }
    }
}
