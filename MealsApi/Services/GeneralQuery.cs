namespace MealsApi.Services
{
    public static class GeneralQuery
    {
        public static string selectAllRecords(string tbName) =>
            "select * from "+ tbName + "";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cols">insert list cols to show</param>
        /// <returns></returns>
        public static string selectAllRecordsWithCustomeCols(string tbName, string[] cols) =>
            "select "+string.Join(",",cols)+" from "+ tbName + "";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">this Id must be as Id</param>
        /// <returns></returns>
        public static string selectOneRecord(string tbName,int Id) =>
            "select * from "+ tbName + " where Id="+ Id + "";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cols"> columns to show </param>
        /// <param name="Id">this Id must be as Id</param>
        /// <returns></returns>
        public static string selectOnRecordsWithCustomeCols(string tbName, string[] cols,int Id) => 
            "select " + string.Join(",", cols) + " from " + tbName + " where Id="+Id+"";

        public static string deleteRecord(string tbName,int Id) =>
            "delete from "+ tbName + " where Id="+ Id + "";
        public static string insertRecord(string tbName, string[] cols, string[] values) => 
            "insert into "+tbName+" ("+string.Join(",",cols)+") values ("+string.Join(",",values)+")";




    }
}
