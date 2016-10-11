Imports System
Imports System.Text
Imports System.IO
Imports System.Collections
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Xml.Schema

Imports Microsoft.BizTalk.ExplorerOM
Imports Microsoft.BizTalk.Deployment
Imports Microsoft.BizTalk.Component.Interop


Namespace ProBizTalk.Samples.PipelineComponents.Utilities
    _
    Public Class SchemaRetriever
        '/ <summary>
        '/ contains a cache of all schemas in the catalog upon first use of Schemas property
        '/ </summary>
        Private Shared _schemas As SchemaCollection
        '/ <summary>
        '/ caches created DocumentSpec instances
        '/ </summary>
        Private Shared _docSpecCache As Hashtable


        '/ <summary>
        '/ initializes used static variables
        '/ </summary>
        Shared Sub New()
            _docSpecCache = New Hashtable()
        End Sub 'New




        '/ <summary>
        '/ provides access to all schemas within the catalog; initialized upon first use
        '/ </summary>

        Public Shared ReadOnly Property Schemas() As SchemaCollection
            Get
                If _schemas Is Nothing Then
                    Dim cdb As New ConfigurationDatabase()

                    Dim explorer As New BtsCatalogExplorer()

                    ' assign the connection string, effectively connecting to the database
                    ' TODO: optimize this if used frequently to cache the connection

                    If cdb.Database.Length = 0 Then
                        explorer.ConnectionString = "Integrated Security=SSPI; Persist Security Info=false; Server=(local); Database=BizTalkMgmtDb;"
                    Else
                        explorer.ConnectionString = cdb.ConnectionString
                    End If
                    _schemas = explorer.Schemas
                End If
                Return _schemas
            End Get
        End Property


        '/ <summary>
        '/ retrieves a requested <see cref="C:Microsoft.BizTalk.Component.Interop.DocumentSpec"/> and loads 
        '/ an example instance, which is returned.
        '/ </summary>
        '/ <param name="schemaName">the schema to be instantiated</param>
        '/ <returns>a sample instance of the retrieved schema</returns>
        Public Shared Function GetInstanceForSchema(ByVal schemaName As String) As System.IO.Stream
            ' defines our return value. null is returned if we fail
            Dim retVal As System.IO.Stream = Nothing

            ' retrieve the requested schema from the ExplorerOM model
            Dim schema As Schema = Schemas(schemaName) '

            ' if the local catalog contains the requested schema
            If Not (schema Is Nothing) Then
                Dim docSchema As DocumentSpec = Nothing

                If _docSpecCache.Contains(schema.AssemblyQualifiedName) Then
                    docSchema = _docSpecCache(schema.AssemblyQualifiedName)
                End If

                Try
                    ' create a DocumentSpec from it to be able to create a correct instance
                    docSchema = New DocumentSpec(schemaName, schema.BtsAssembly.DisplayName)
                    _docSpecCache.Add(schema.AssemblyQualifiedName, docSchema)
                Catch
                End Try
                Dim sb As New StringBuilder()

                ' if the DocumentSpec was correctly instantiated (i.e. the schema Assembly was found)
                If Not (docSchema Is Nothing) Then
                    Dim writer As New StringWriter(sb)
                    Try
                        ' instantiate our return value


                        ' create and instance of the schema and load it into our XmlDocument instance
                        retVal = docSchema.CreateXmlInstance(writer)
                    Finally
                        writer.Dispose()
                    End Try
                End If
            End If

            ' return null if nothing was found or the instance otherwise
            Return retVal

        End Function
    End Class
End Namespace