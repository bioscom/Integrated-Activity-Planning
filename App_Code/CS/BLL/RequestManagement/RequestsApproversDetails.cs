using System.Data;

/// <summary>
/// Summary description for RequestsApproversDetails
/// </summary>
//public class RequestsApproversDetails
//{
//    public RequestsApproversDetails()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }
//}

/// <summary>
/// Summary description for Approvers' Comments
/// </summary>
public class ApproversComment
{
    public long m_lRequestId { get; set; }
    public int m_iCommentId { get; set; }
    public string m_sComments { get; set; }
    public string m_sCommentDate { get; set; }
    public int m_iStatus { get; set; }
    public int m_iUserId { get; set; }
    public string m_sFullName { get; set; }
    public string m_sDateReceived { get; set; }
    public string m_sTimeReceived { get; set; }
    public string m_sTimeComments { get; set; }

    public ApproversComment()
    {

    }

    public ApproversComment(DataRow dr)
    {
        m_lRequestId = long.Parse(dr["IDREQUEST"].ToString());
        m_iCommentId = int.Parse(dr["COMMENTID"].ToString());
        m_sComments = dr["COMMENTS"].ToString();
        m_sCommentDate = dr["COMMENTSDATE"].ToString();
        m_iStatus = int.Parse(dr["STATUS"].ToString());
        m_iUserId = int.Parse(dr["IDUSER"].ToString());
        m_sFullName = dr["FULLNAME"].ToString();
        m_sDateReceived = dr["DATE_RECEIVED"].ToString();
        m_sTimeReceived = dr["TIMERECEIVED"].ToString();
        m_sTimeComments = dr["TIMECOMMENTS"].ToString();
    }
}