dim objServices, objMsg, svcinsts, inst, msg, ndx, size, nHostCount

Dim aryClassIDs()
Dim aryTypeIDs()
Dim aryInstanceIDs()

Dim aryClassIDsTemp()
Dim aryTypeIDsTemp()
Dim aryInstanceIDsTemp()
'-----------------------------------------------
Dim strKey2Instance
Dim strQuery2Msg
Dim objQueue

'-----------------------------------------------
'-- Creating and opening log file to append info
'-----------------------------------------------
Dim strDirectory, strFile, objFSO, objTextFile
strDirectory = "c:"
strFile = "\TermallScriptResults.txt"
'Create the File System Object
Set objFSO = CreateObject("Scripting.FileSystemObject")

'Check that the folder and file exists
If objFSO.FileExists(strDirectory & strFile) Then
    'OpenTextFile Method needs a Const value
    'ForAppending = 8 ForReading = 1, ForWriting = 2
    Const ForAppending = 8
    Set objTextFile = objFSO.OpenTextFile(strDirectory & strFile,
                                          ForAppending, True)
Else
    Set objTextFile = objFSO.CreateTextFile(strDirectory & strFile)
End If

'---------------------------
'-- Set the object reference
'---------------------------
set objServices = GetObject("winmgmts:\\.\root\MicrosoftBizTalkServer")

'--------------------------------------------------------------------
'-- Create array of hostnames to loop over hosts
'-- increase the array's size, 
'-- add as many hostnames as required to the array,
'-- and initialize the corresponding aryHostSuspendedMessages entries
'--------------------------------------------------------------------
Dim aryHostNames(1)
Dim aryHostSuspsendedMessages(1)
aryHostNames(0) = "BizTalkServerApplication"
aryHostNames(1) = "BizTalkServerIsolatedHost"
aryHostSuspsendedMessages(0) = 0
aryHostSuspsendedMessages(1) = 0
'--------------------------------------------------------------------------
'-- The maximum number of suspended instances to clean up in a single batch
'--------------------------------------------------------------------------
Dim nMaxInstancesToClean
nMaxInstancesToClean = 500


Dim strHost
nHostCount = 0

objTextFile.WriteLine("------- SCRIPT EXECUTION STARTED -------")

'--------------------------------------------------------------------
'-- Terminate instance in each suspended Queue for the selected hosts
'--------------------------------------------------------------------
for each strHost in aryHostNames

    'wscript.echo "Host: " & strHost
    objTextFile.WriteLine("----------------------------------------")
    objTextFile.WriteLine(Now() & " :    Host: " & strHost)

'--------------------------------
'-- Query for Suspended instances
'--------------------------------
    wbemFlagReturnImmediately = 16
    wbemFlagForwardOnly = 32
    IFlags = wbemFlagReturnImmediately + wbemFlagForwardOnly
    set svcinsts = objServices.ExecQuery(
             "select * from MSBTS_serviceinstance
              where (servicestatus=32 or servicestatus=4)
              and HostName="""&strHost&"""",, IFlags)

    'Using a semi-synchronous therefore the count property doesn't
    'work with wbemFlagForwardOnly
    'size = svcinsts.Count
    'wscript.echo "Suspended Message: " & size

    strKey2Instance = "MSBTS_HostQueue.HostName=""" & strHost & """"

    set objQueue = objServices.Get(strKey2Instance)

    If Err <> 0 Then

        wscript.echo Now() & " :    Failed to get MSBTS_HostQueue instance"
        wscript.echo Now() & " :    " & Err.Description & Err.Number
        objTextFile.WriteLine(Now() & " :
                              Failed to get MSBTS_HostQueue instance")
        objTextFile.WriteLine(Now() & " :    " + Err.Description & Err.Number)

    Else

        ndx = 0

        redim aryClassIDs(nMaxInstancesToClean)
        redim aryTypeIDs(nMaxInstancesToClean)
        redim aryInstanceIDs(nMaxInstancesToClean)

        'Loop through all instances and terminate nMaxInstancesToClean at a
        'time.
        'This number was choosen for optimization, so it can be changed if
        'desired.
        for each inst in svcinsts

             If ndx > nMaxInstancesToClean Then
                  'Currently 500 entries are ready to be terminated

                  'wscript.echo "Attempting to terminate "
                  '& ndx & " suspended instances in host"
                  objTextFile.WriteLine(Now() &
                         " :    Attempting to terminate "
                         & ndx & " suspended instances in host")

                  objQueue.TerminateServiceInstancesByID aryClassIDs,
                                    aryTypeIDs, aryInstanceIDs

                  If Err <> 0 Then
                       wscript.echo Now() & " :    Terminate failed"
                       wscript.echo Now() & " :    " & Err.Description
                                          & Err.Number
                       objTextFile.WriteLine(Now() & " :    Terminate failed")
                       objTextFile.WriteLine(Now() & " :    " + Err.Description
                                                   & Err.Number)
                  Else
                       'wscript.echo "SUCCESS> " & ndx &
                       '" Service instance terminated"
                       objTextFile.WriteLine(Now() &
                          " :    SUCCESS> " & ndx &
                          " Service instance terminated")
                  End If

                  'Reinitialize the arrays and counter
                  'to ensure we store non-terminated
                  'Entries for the next round of termination
                  ndx = 0
                  redim aryClassIDs(nMaxInstancesToClean)
                  redim aryTypeIDs(nMaxInstancesToClean)
                  redim aryInstanceIDs(nMaxInstancesToClean)

                  'Suspends script execution for 30 seconds,
                  'then continues execution
                  'wscript.echo "Suspending script execution for 30 seconds"
                  objTextFile.WriteLine(Now() &
                    " :    Suspending script execution for 30 seconds")
                  Wscript.Sleep 30000

             End If

             aryClassIDs(ndx) = inst.Properties_("ServiceClassId")
             aryTypeIDs(ndx) = inst.Properties_("ServiceTypeId")
             aryInstanceIDs(ndx) = inst.Properties_("InstanceId")

             ndx = ndx + 1

        next

        'If count <> zero then the arrays are still populated
        'and the messages need to be terminated one last time.
        If ( ndx > 0 ) then

             redim aryClassIDsTemp(ndx-1)
             redim aryTypeIDsTemp(ndx-1)
             redim aryInstanceIDsTemp(ndx-1)

             for i=1 to ndx
                 aryClassIDsTemp(i-1) = aryClassIDs(i-1)
                 aryTypeIDsTemp(i-1) = aryTypeIDs(i-1)
                 aryInstanceIDsTemp(i-1) = aryInstanceIDs(i-1)
                 aryHostSuspsendedMessages(nHostCount) =
                    aryHostSuspsendedMessages(nHostCount) + 1
             next

             'wscript.echo "Attempting to terminate " &
             'ndx & " suspended instances in host"
             objTextFile.WriteLine(Now() &
                  " :    Attempting to terminate " & ndx &
                  " suspended instances in host")

             objQueue.TerminateServiceInstancesByID aryClassIDsTemp,
                       aryTypeIDsTemp, aryInstanceIDsTemp

             If Err <> 0 Then
                 wscript.echo Now() & " :    Terminate failed"
                 wscript.echo Now() & " :    " & Err.Description & Err.Number
                 objTextFile.WriteLine(Now() & " :    Terminate failed")
                 objTextFile.WriteLine(Now() &
                                " :    " + Err.Description & Err.Number)
             Else
                 'wscript.echo "SUCCESS> " & ndx &
                 '" Service instance terminated"
                 objTextFile.WriteLine(Now() &
                    " :    SUCCESS> " & ndx & " Service instance terminated")
             End If
        Else
             'wscript.echo "No suspended instances in this host"
             objTextFile.WriteLine(Now() &
                     " :    No suspended instances in this host")
        End If
    End If

    nHostCount = nHostCount + 1

Next

objTextFile.WriteLine("")
objTextFile.WriteLine("-------       SUMMARY START      -------")

nHostCount = 0
for each strHost in aryHostNames
    wscript.echo Now() & " :    Total of " &
                 aryHostSuspsendedMessages(nHostCount) &
                 " suspended messages were terminated in " &
                 strHost & " host"
    objTextFile.WriteLine(Now() & " :    Total of " &
                          aryHostSuspsendedMessages(nHostCount) &
                          " suspended messages were terminated in " &
                          strHost & " host")
    nHostCount = nHostCount + 1
Next
objTextFile.WriteLine("-------        SUMMARY END       -------")
objTextFile.WriteLine("")

wscript.echo Now() & " :    Script Execution Completed"
objTextFile.WriteLine(Now() & " :    Script Execution Completed")
objTextFile.WriteLine("-------  SCRIPT EXECUTION ENDED  -------")
objTextFile.Close
Set objTextFile = nothing
