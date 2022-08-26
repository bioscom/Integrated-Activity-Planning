using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class UserControl_oRequestArchive : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //PopulateTreeViewControl();
            PopulateRadTreeViewControl();
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    private void PopulateRadTreeViewControl()
    {
        try
        {
            RadTreeNode parentNode = null;

            var olstYr = RequestFormRequirement.LoadYears();
            foreach (Yearly o in olstYr)
            {
                parentNode = new RadTreeNode(o.m_iYear.ToString(), o.m_iYear.ToString());

                var olst = RequestFormRequirement.LoadMonths();
                foreach (Monthly oM in olst)
                {
                    var parentNode2 = new RadTreeNode(oM.m_sMonth, oM.m_iMonth.ToString());
                    parentNode.Nodes.Add(parentNode2);

                    var lstRequests = RequestFormRequirement.lstGetRequestByMonthYear(o.m_iYear, oM.m_iMonth);
                    foreach (RequestLst oR in lstRequests)
                    {
                        var childNode = new RadTreeNode(oR.m_sRequestNo + " - " + oR.m_sProject, oR.m_lRequestId.ToString());
                        parentNode2.Nodes.Add(childNode);
                    }
                }
                parentNode.ExpandMode = TreeNodeExpandMode.ClientSide; //.ExpandChildNodes();
                mnuRadTreeView.Nodes.Add(parentNode);
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    //private void PopulateTreeViewControl()
    //{
    //    try
    //    {
    //        TreeNode parentNode = null;

    //        var olstYr = RequestFormRequirement.LoadYears();
    //        foreach (Yearly o in olstYr)
    //        {
    //            parentNode = new TreeNode(o.m_iYear.ToString(), o.m_iYear.ToString());

    //            var olst = RequestFormRequirement.LoadMonths();
    //            foreach (Monthly oM in olst)
    //            {
    //                var parentNode2 = new TreeNode(oM.m_sMonth, oM.m_iMonth.ToString());
    //                parentNode.ChildNodes.Add(parentNode2);

    //                var lstRequests = RequestFormRequirement.lstGetRequestByMonthYear(o.m_iYear, oM.m_iMonth);
    //                foreach (RequestLst oR in lstRequests)
    //                {
    //                    var childNode = new TreeNode(oR.m_sRequestNo + " - " + oR.m_sProject, oR.m_lRequestId.ToString());
    //                    parentNode2.ChildNodes.Add(childNode);
    //                }
    //            }
    //            parentNode.Expand();
    //            mnuTreeView.Nodes.Add(parentNode);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        appMonitor.logAppExceptions(ex);
    //        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
    //    }
    //}

    protected void mnuTreeView_NodeExpand(object sender, TreeNodeEventArgs e)
    {
        if (e.Node.ChildNodes.Count == 0)

            PopulateNodeOnDemand(e, TreeNodeExpandMode.ServerSide, e.Node.Parent, e.Node);
    }

    private static void PopulateNodeOnDemand(TreeNodeEventArgs e, TreeNodeExpandMode expandMode, TreeNode PN, TreeNode CN)
    {
        //var lstRequests = RequestFormRequirement.lstGetRequestByMonthYear(PN.Value o.m_iYear, oM.m_iMonth);
        var lstRequests = RequestFormRequirement.lstGetRequestByMonthYear(int.Parse(PN.Value), int.Parse(CN.Value));
        foreach (RequestLst oR in lstRequests)
        {
            var childNode = new TreeNode(oR.m_sRequestNo + " - " + oR.m_sProject, oR.m_lRequestId.ToString());
            e.Node.ChildNodes.Add(childNode);
        }

        //DataTable data = GetChildNodes(e.Node.Value);

        //foreach (DataRow row in data.Rows)
        //{
        //    RadTreeNode node = new RadTreeNode();
        //    node.Text = row["Title"].ToString();
        //    node.Value = row["CategoryId"].ToString();

        //    if (Convert.ToInt32(row["ChildrenCount"]) > 0)
        //    {
        //        node.ExpandMode = expandMode;
        //    }
        //    e.Node.Nodes.Add(node);
        //}
        e.Node.Expanded = true;
    }


    //protected void mnuTreeView_SelectedNodeChanged(object sender, EventArgs e)
    //{
    //    long lRequestId = long.Parse(mnuTreeView.SelectedNode.Value);


    //}
}