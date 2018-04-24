﻿Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler
Imports DevExpress.Web.ASPxScheduler.Internal
Imports System.Drawing

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Private ReadOnly Property Storage() As ASPxSchedulerStorage
        Get
            Return Me.ASPxScheduler1.Storage
        End Get
    End Property
    Public Shared RandomInstance As New Random()



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        SetupMappings()
        ResourceFiller.FillResources(Me.ASPxScheduler1.Storage, 3)
        ASPxScheduler1.AppointmentDataSource = appointmentDataSource
        ASPxScheduler1.DataBind()


    End Sub

    #Region "Data Fill"


    Private Function GetCustomEvents() As CustomEventList
        Dim events As CustomEventList = TryCast(Session("ListBoundModeObjects"), CustomEventList)
        If events Is Nothing Then
            events = GenerateCustomEventList()
            Session("ListBoundModeObjects") = events
        End If
        Return events
    End Function

    #Region "Random events generation"
    Private Function GenerateCustomEventList() As CustomEventList
        Dim eventList As New CustomEventList()
        Dim count As Integer = Storage.Resources.Count
        For i As Integer = 0 To count - 1
            Dim resource As Resource = Storage.Resources(i)
            Dim subjPrefix As String = resource.Caption & "'s "

            eventList.Add(CreateEvent(resource.Id, subjPrefix & "meeting", 2, 5))
            eventList.Add(CreateEvent(resource.Id, subjPrefix & "travel", 3, 6))
            eventList.Add(CreateEvent(resource.Id, subjPrefix & "phone call", 0, 10))
        Next i
        Return eventList
    End Function
    Private Function CreateEvent(ByVal resourceId As Object, ByVal subject As String, ByVal status As Integer, ByVal label As Integer) As CustomEvent
        Dim customEvent As New CustomEvent()
        customEvent.Subject = subject
        customEvent.OwnerId = resourceId
        Dim rnd As Random = RandomInstance
        Dim rangeInHours As Integer = 48
        customEvent.StartTime = Date.Today + TimeSpan.FromHours(rnd.Next(0, rangeInHours))
        customEvent.EndTime = customEvent.StartTime.Add(TimeSpan.FromHours(rnd.Next(0, rangeInHours \ 8)))
        customEvent.Status = status
        customEvent.Label = label
        customEvent.Id = "ev" & customEvent.GetHashCode()
        Return customEvent
    End Function
    #End Region


    Private Sub SetupMappings()
        Dim mappings As ASPxAppointmentMappingInfo = Storage.Appointments.Mappings
        Storage.BeginUpdate()
        Try
            mappings.AppointmentId = "Id"
            mappings.Start = "StartTime"
            mappings.End = "EndTime"
            mappings.Subject = "Subject"
            mappings.AllDay = "AllDay"
            mappings.Description = "Description"
            mappings.Label = "Label"
            mappings.Location = "Location"
            mappings.RecurrenceInfo = "RecurrenceInfo"
            mappings.ReminderInfo = "ReminderInfo"
            mappings.ResourceId = "OwnerId"
            mappings.Status = "Status"
            mappings.Type = "EventType"
        Finally
            Storage.EndUpdate()
        End Try

    End Sub
    #End Region

    #Region "Data Bind"
    Protected Sub ASPxScheduler1_AppointmentInserting(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs)

        Dim storage_Renamed As ASPxSchedulerStorage = DirectCast(sender, ASPxSchedulerStorage)
        Dim apt As Appointment = CType(e.Object, Appointment)
        storage_Renamed.SetAppointmentId(apt, "a" & apt.GetHashCode())
    End Sub

    Protected Sub appointmentsDataSource_ObjectCreated(ByVal sender As Object, ByVal e As ObjectDataSourceEventArgs)
        e.ObjectInstance = New CustomEventDataSource(GetCustomEvents())
    End Sub

    #End Region

    Protected Sub ASPxScheduler1_PrepareAppointmentFormPopupContainer(ByVal sender As Object, ByVal e As ASPxSchedulerPrepareFormPopupContainerEventArgs)
        e.Popup.HeaderText = "ASPxScheduler1_PrepareAppointmentFormPopupContainer"
        e.Popup.Width = Unit.Pixel(800)
        e.Popup.BackColor = Color.Wheat
        e.Popup.ContentStyle.Paddings.Padding = Unit.Pixel(0)

    End Sub

End Class
