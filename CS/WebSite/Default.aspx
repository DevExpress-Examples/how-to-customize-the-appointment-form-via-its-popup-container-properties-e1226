﻿<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.0.0, Culture=neutral, PublicKeyToken=79868b8147b5eae4"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.0.0, Culture=neutral, PublicKeyToken=79868b8147b5eae4"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register assembly="DevExpress.Web.ASPxScheduler.v15.2, Version=15.2.0.0, Culture=neutral, PublicKeyToken=79868b8147b5eae4" namespace="DevExpress.Web.ASPxScheduler" tagprefix="dxwschs" %>
<%@ Register assembly="DevExpress.XtraScheduler.v15.2.Core, Version=15.2.0.0, Culture=neutral, PublicKeyToken=79868b8147b5eae4" namespace="DevExpress.XtraScheduler" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <dxp:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
            <PanelCollection>
                <dxp:PanelContent runat="server" _designerRegion="0">
                    &nbsp;&nbsp;
                    <br />
        <dxwschs:ASPxScheduler id="ASPxScheduler1" runat="server" Width="100%" GroupType="Resource" OnAppointmentInserting="ASPxScheduler1_AppointmentInserting" OnPrepareAppointmentFormPopupContainer="ASPxScheduler1_PrepareAppointmentFormPopupContainer">         
        <Views>
            <DayView ResourcesPerPage="1" DayCount="3"><TimeRulers><cc1:TimeRuler /></TimeRulers></DayView>
            <WorkWeekView><TimeRulers><cc1:TimeRuler /></TimeRulers></WorkWeekView>
            <WeekView ResourcesPerPage="1">
            </WeekView>
        </Views>
            <OptionsBehavior RemindersFormDefaultAction="Custom" ShowRemindersForm="False" />
    </dxwschs:ASPxScheduler>
                </dxp:PanelContent>
            </PanelCollection>
        </dxp:ASPxPanel>
        &nbsp;
<asp:ObjectDataSource ID="appointmentDataSource" runat="server" DataObjectTypeName="CustomEvent" TypeName="CustomEventDataSource" DeleteMethod="DeleteMethodHandler" SelectMethod="SelectMethodHandler" InsertMethod="InsertMethodHandler" UpdateMethod="UpdateMethodHandler" OnObjectCreated="appointmentsDataSource_ObjectCreated" />

    </form>

</body>
</html>