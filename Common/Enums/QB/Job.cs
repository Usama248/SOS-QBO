namespace Common.Enums.QB
{
    public enum JobStatusEnum
    {
        Scheduled = 1,
        InProgress,
        SyncingError,
        Canceled,
        Completed
    }
    public enum SyncingTypeEnum
    {
        QBO = 1,
        QBD
    }
    public enum SyncingDirectionEnum
    {
        ToProjul = 1,
        FromProjul
    }
    public enum EntityTypeEnum
    {
        Customer = 1,
    }
    public enum EntityStatusEnum
    {
        New = 1,
        Process,
        Completed,
        Error,
        QueryFileUploaded,
        QueryResponseFileUploaded,
        AddUpdateRequestFileUploaded,
        AddUpdateResponseFileUploaded,
    }
    public enum SyncingStatusEnum
    {
        Scheduled,
        Pending,
        SyncError,
        SyncOk,
        QueryGenerated,
        AddUpdateQueryGenerated
    }
}
