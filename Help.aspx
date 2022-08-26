<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Help.aspx.cs" Inherits="_Help" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Integrated Activity Planning</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <style>
<!--table
	{mso-displayed-decimal-separator:"\.";
	mso-displayed-thousand-separator:"\,";}
@page
	{margin:.35in .48in .37in .54in;
	mso-header-margin:.5in;
	mso-footer-margin:.5in;}
tr
	{mso-height-source:auto;}
col
	{mso-width-source:auto;}
br
	{mso-data-placement:same-cell;}
.style0
	{mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	white-space:nowrap;
	mso-rotate:0;
	mso-background-source:auto;
	mso-pattern:auto;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	border:none;
	mso-protection:locked visible;
	mso-style-name:Normal;
	mso-style-id:0;}
td
	{mso-style-parent:style0;
	padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border:none;
	mso-background-source:auto;
	mso-pattern:auto;
	mso-protection:locked visible;
	white-space:nowrap;
	mso-rotate:0;}
.xl24
	{mso-style-parent:style0;
	font-family:Verdana, sans-serif;
	mso-font-charset:0;
	vertical-align:top;}
.xl25
	{mso-style-parent:style0;
	font-family:"Times New Roman", serif;
	mso-font-charset:0;
	text-align:left;
	vertical-align:top;}
.xl26
	{mso-style-parent:style0;
	text-align:left;
	white-space:normal;}
.xl27
	{mso-style-parent:style0;
	white-space:normal;}
.xl28
	{mso-style-parent:style0;
	font-family:Verdana, sans-serif;
	mso-font-charset:0;
	vertical-align:top;
	white-space:normal;}
.xl29
	{mso-style-parent:style0;
	font-family:Verdana, sans-serif;
	mso-font-charset:0;
	text-align:left;
	vertical-align:top;
	white-space:normal;}
.xl30
	{mso-style-parent:style0;
	vertical-align:top;
	white-space:normal;}
.xl31
	{mso-style-parent:style0;
	font-family:Verdana, sans-serif;
	mso-font-charset:0;
	text-align:left;
	white-space:normal;}
.xl32
	{mso-style-parent:style0;
	text-decoration:underline;
	text-underline-style:single;
	font-family:Verdana, sans-serif;
	mso-font-charset:0;
	text-align:left;
	white-space:normal;}
.xl33
	{mso-style-parent:style0;
	font-family:Verdana, sans-serif;
	mso-font-charset:0;
	text-align:left;
	border:.5pt solid windowtext;
	white-space:normal;}
.xl34
	{mso-style-parent:style0;
	border:.5pt solid windowtext;
	white-space:normal;}
.xl35
	{mso-style-parent:style0;
	font-family:Verdana, sans-serif;
	mso-font-charset:0;
	text-align:left;
	vertical-align:top;
	border:.5pt solid windowtext;
	white-space:normal;}
.xl36
	{mso-style-parent:style0;
	font-family:Verdana, sans-serif;
	mso-font-charset:0;
	vertical-align:top;
	border:.5pt solid windowtext;
	white-space:normal;}
.xl37
	{mso-style-parent:style0;
	vertical-align:top;
	border:.5pt solid windowtext;
	white-space:normal;}
.xl38
	{mso-style-parent:style0;
	font-family:Verdana, sans-serif;
	mso-font-charset:0;
	mso-number-format:"Short Date";
	vertical-align:top;
	border:.5pt solid windowtext;
	white-space:normal;}
.xl39
	{mso-style-parent:style0;
	text-align:left;
	border:.5pt solid windowtext;
	white-space:normal;}
.xl40
	{mso-style-parent:style0;
	vertical-align:middle;
	border:.5pt solid windowtext;
	white-space:normal;}
.xl41
	{mso-style-parent:style0;
	font-weight:700;
	font-family:Verdana, sans-serif;
	mso-font-charset:0;
	text-align:left;
	border:.5pt solid windowtext;
	white-space:normal;}
.xl42
	{mso-style-parent:style0;
	font-weight:700;
	border:.5pt solid windowtext;
	white-space:normal;}
-->
</style>
<br />
<table x:str border=1 cellpadding=1 cellspacing=1  style='border-collapse:collapse;table-layout:fixed;width:80%; margin-left:5em'>
 <col class=xl26 width=340 style='mso-width-source:userset;mso-width-alt:12434;
 width:255pt'>
 <col class=xl27 width=491 style='mso-width-source:userset;mso-width-alt:17956;
 width:368pt'>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl41 width=340 style='width:255pt' x:str="Actions  ">Actions<span
  style="mso-spacerun: yes">  </span></td>
  <td class=xl42 width=491 style='border-left:none;width:368pt'
  x:str="Description ">Description </td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl33 width=340 style='border-top:none;width:255pt'>To be filled by
  change request controller</td>
  <td class=xl34 width=491 style='border-top:none;border-left:none;width:368pt'>In
  charge of change control forms update and maintenance</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>CHANGE REQUEST
  NOS</td>
  <td class=xl36 width=491 style='border-top:none;border-left:none;width:368pt'>Manual
  or auto generated serial numbers</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>APPROVAL STATUS</td>
  <td class=xl36 width=491 style='border-top:none;border-left:none;width:368pt'>Either
  approved or not approved</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>AREA:</td>
  <td class=xl36 width=491 style='border-top:none;border-left:none;width:368pt'
  x:str="Area, Asset or Functional Team where change is taking place ">Area,
  Asset or Functional Team where change is taking place </td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>PLAN TYPE:</td>
  <td class=xl36 width=491 style='border-top:none;border-left:none;width:368pt'>Type
  of activity plan, wells or facility shutdown etc</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>LOCATION:</td>
  <td class=xl36 width=491 style='border-top:none;border-left:none;width:368pt'>geographical
  location of activity to be executed</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>PLAN ISSUE:</td>
  <td class=xl36 width=491 style='border-top:none;border-left:none;width:368pt'>Plan
  reference point (BP-xx, IAP, etc)</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>PROJECT/ACTIVITY:</td>
  <td class=xl36 width=491 style='border-top:none;border-left:none;width:368pt'>The
  project or activity name</td>
 </tr>
 <tr height=35 style='mso-height-source:userset;height:26.25pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>CHANGE TYPE:</td>
  <td class=xl36 width=491 style='border-top:none;border-left:none;width:368pt'>Tyoe
  of change required (replacement, removal, injection, extension etc.)</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>ORIGINATOR:&nbsp;</td>
  <td class=xl36 width=491 style='border-top:none;border-left:none;width:368pt'>The
  activity executor that originates the project</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>WO NO (Req):</td>
  <td class=xl37 width=491 style='border-top:none;border-left:none;width:368pt'
  x:str="SAP workorder number ">SAP workorder number </td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>REQUEST DATE:</td>
  <td class=xl38 width=491 style='border-top:none;border-left:none;width:368pt'>Date
  activity is requested to be executed</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>DESCRIPTION OF
  CHANGE REQUEST</td>
  <td class=xl34 width=491 style='border-top:none;border-left:none;width:368pt'>General
  description of the change reauest</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>DESCRIPTION OF
  REASONS FOR CHANGE</td>
  <td class=xl34 width=491 style='border-top:none;border-left:none;width:368pt'>Descriptions
  of the reasons adduced for changes to plan</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>JUSTIFICATION
  FOR CHANGE</td>
  <td class=xl34 width=491 style='border-top:none;border-left:none;width:368pt'>Reason
  why change must be approved</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>TECHNICAL
  INTERGRITY:</td>
  <td class=xl34 width=491 style='border-top:none;border-left:none;width:368pt'>Condition
  of the integrity of the asset the calls for a change control</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>HSE:</td>
  <td class=xl34 width=491 style='border-top:none;border-left:none;width:368pt'>Condition
  of the HSE the neccessitates change control</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>OPERATIONAL/PRODUCTION:</td>
  <td class=xl34 width=491 style='border-top:none;border-left:none;width:368pt'>Production
  loss/gain or operational reasons</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>BUSINESS IMPACT
  (SAVINGS/LOSS):</td>
  <td class=xl34 width=491 style='border-top:none;border-left:none;width:368pt'
  x:str="Impact on business ">Impact on business </td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>IMPACT OF CHANGE</td>
  <td class=xl34 width=491 style='border-top:none;border-left:none;width:368pt'
  x:str="Magnitude of impact of change ">Magnitude of impact of change </td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>ACTIVITIES
  NEGATIVELY IMPACTED BY CHANGE</td>
  <td class=xl34 width=491 style='border-top:none;border-left:none;width:368pt'>List
  of activities that are affected negatively by the change</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl39 width=340 style='border-top:none;width:255pt'>PRODUCTION
  IMPACT</td>
  <td class=xl34 width=491 style='border-top:none;border-left:none;width:368pt'>Production
  volume to be generated or deffered as a result of change</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>ACTIONS REQUIRED
  FOR IMPACTED ACTIVITIES:</td>
  <td class=xl34 width=491 style='border-top:none;border-left:none;width:368pt'>What
  needs to be done on the activities impacted</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>RECOVERY PLAN:</td>
  <td class=xl34 width=491 style='border-top:none;border-left:none;width:368pt'>What
  are the plans to recover from the effects of the change</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>EVALUATION OF
  CHANGE FOR STRATEGIC FIT</td>
  <td class=xl34 width=491 style='border-top:none;border-left:none;width:368pt'>Indices
  considered to effect change</td>
 </tr>
 <tr height=43 style='mso-height-source:userset;height:32.25pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>COMMENTS FROM
  OTHER ACTIVITY 
      <br />
      EXECUTORS IMPACTED</td>
  <td class=xl34 width=491 style='border-top:none;border-left:none;width:368pt'>Other
  executors that are impacted by the activity must agredd to the change and
  should make comments.</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>Activity
  Executor</td>
  <td class=xl40 width=491 style='border-top:none;border-left:none;width:368pt'>Comments</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>CHANGE
  CHAMPIONS:</td>
  <td class=xl36 width=491 style='border-top:none;border-left:none;width:368pt'>every
  one responsible for change to happen</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl35 width=340 style='border-top:none;width:255pt'>Activity Owner:</td>
  <td class=xl36 width=491 style='border-top:none;border-left:none;width:368pt'>The
  originator and executor of the activity</td>
 </tr>
 <tr height=26 style='mso-height-source:userset;height:20.1pt'>
  <td class=xl29></td>
  <td class=xl28></td>
 </tr>
 <![if supportMisalignedColumns]>
 <![endif]>
</table>
    </div>
    </form>
</body>
</html>
