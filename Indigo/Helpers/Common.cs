namespace Indigo.Helpers
{
    public static class Common
    {
        public static bool IsValidId(this int? id)
        {
            if (id < 1 || id == null) return false;

            return true;
        }
    }
}
