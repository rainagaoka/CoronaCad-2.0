Imports System.Globalization
Imports System.IO
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.Colors
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.Runtime
Imports acApp = Autodesk.AutoCAD.ApplicationServices.Application

Public Class Formulario_CoronaCad
    Dim doc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
    Dim db As Database = doc.Database
    Dim ed As Editor = doc.Editor

    Dim listaArquivos() As String = {}
    Dim listaBrasil() As String = {"Brasil"}
    Dim listaMunicipios As New ArrayList
    Dim listaEstados As New ArrayList
    Dim listaDados As New List(Of Dados)
    Dim tipoDivisao As String
    Dim tipoDados As String

    Dim diaInicial As Date
    Dim diaFinal As Date
    Dim dadoMin As Integer
    Dim dadoMax As Integer
    Dim distorcaoX As Double
    Dim distorcaoY As Double
    Dim ultimoEixoY As Integer

    Dim objetosCriados As New ObjectIdCollection

    <CommandMethod("CoronaCad4")>
    Public Sub CoronaCad4()
        'abrir o formulario
        Me.ShowDialog()
    End Sub

    'classe para criar a lista com os dados
    Public Class Dados
        Public Property Nome As String
        Public Property Data As DateTime
        Public Property Dados As Integer
    End Class
    Private Sub GerarDados()
        Try
            'limpa a lista dos dados
            listaDados.Clear()

            'numero das colunas com os nomes e com os dados
            Dim numColDivisao As Integer
            Dim numColTipoDados As Integer

            'escolher qual coluna pegar o nome da divisao
            For Each radiobutton As RadioButton In GroupBox_divisao.Controls.OfType(Of RadioButton)
                If radiobutton.Checked Then
                    Select Case radiobutton.Text
                        Case "Brasil"
                            numColDivisao = 0
                            tipoDivisao = "Brasil"
                        Case "Por Estado"
                            numColDivisao = 1
                            tipoDivisao = "Por Estado"
                        Case "Por Município"
                            numColDivisao = 2
                            tipoDivisao = "Por Município"
                    End Select
                End If
            Next

            'escolher qual coluna pegar os dados
            For Each radiobutton As RadioButton In GroupBox_Dados.Controls.OfType(Of RadioButton)
                If radiobutton.Checked Then
                    Select Case radiobutton.Text
                        Case "Casos Novos"
                            numColTipoDados = 11
                            tipoDados = "Casos Novos"
                        Case "Casos Acumulados"
                            numColTipoDados = 10
                            tipoDados = "Casos Acumulados"
                        Case "Óbitos Novos"
                            numColTipoDados = 13
                            tipoDados = "Óbitos Novos"
                        Case "Óbitos Acumulados"
                            numColTipoDados = 12
                            tipoDados = "Óbitos Acumulados"
                        Case "Novos Recuperados"
                            numColTipoDados = 14
                            tipoDados = "Novos Recuperados"
                    End Select
                    Exit For
                End If
            Next

            'mostra progress bar
            ProgressBar.Show()

            'percorrer todos os arquivos
            For Each arquivo In listaArquivos
                ProgressBar.Value = 0

                'abrindo um arquivo
                Dim fs = New FileStream(arquivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Using sr = New StreamReader(fs)
                    Dim linha As String

                    'ler a primeira linha do arquivo para pular os nomes das colunas
                    sr.ReadLine()

                    Do
                        'pegar proxima linha do arquivo
                        linha = sr.ReadLine()
                        'verifica se o arquivo terminou
                        If (linha IsNot Nothing) Then
                            Try
                                'ler cada linha, separa por ";" e adicionar os dados na lista de dados
                                Dim itensLinha() = linha.Split(";")
                                Dim dataLinha As DateTime

                                'verifica se a data não esta no formato aaaa-mm-dd ou dd/mm/aaaa
                                If itensLinha(7).Contains("-") Then
                                    dataLinha = DateTime.ParseExact(itensLinha(7), "yyyy-MM-dd", CultureInfo.InvariantCulture)
                                ElseIf itensLinha(7).Contains("/") Then
                                    dataLinha = DateTime.ParseExact(itensLinha(7), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                                End If

                                'verifica se a linha tem dados de qual divisão, se não for a que foi selecionado pula
                                Select Case tipoDivisao
                                    Case "Brasil"
                                        If Not listaBrasil.Contains(itensLinha(0)) Then
                                            Continue Do
                                        End If
                                    Case "Por Estado"
                                        If Not ComboBox_Estados.SelectedItem = itensLinha(1) Or itensLinha(2).Length > 1 Then
                                            Continue Do
                                        End If
                                    Case "Por Município"
                                        If Not ComboBox_Municipios.SelectedItem = itensLinha(2) Then
                                            Continue Do
                                        End If
                                End Select

                                'pega o dado da linha e adiciona a lista
                                Dim linhaDados = New Dados With {
                                    .Nome = itensLinha(numColDivisao),
                                    .Data = dataLinha
                                }
                                Try
                                    linhaDados.Dados = itensLinha(numColTipoDados)
                                Catch
                                    linhaDados.Dados = 0
                                End Try
                                listaDados.Add(linhaDados)
                            Catch
                            End Try
                        End If

                    Loop Until linha Is Nothing

                    'atualizad o progress bar
                    ProgressBar.Value = sr.BaseStream.Position / sr.BaseStream.Length * 100
                    sr.Close()
                End Using
            Next

            'esconde o progressBar
            ProgressBar.Hide()

            'atribui os dados encontrados na lista
            diaInicial = listaDados.Min(Function(i) i.Data)
            diaFinal = listaDados.Max(Function(i) i.Data)
            dadoMin = listaDados.Min(Function(i) i.Dados)
            dadoMax = listaDados.Max(Function(i) i.Dados)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub DesenharGráfico()
        'pega o documento do autocad
        Dim doc As Document = acApp.DocumentManager.MdiActiveDocument
        'pega a database do arquivo
        Dim db As Database = doc.Database

        Try
            Using tr = doc.TransactionManager.StartTransaction
                'tabela de bloco em escrita
                Dim bt As BlockTable = db.BlockTableId.GetObject(OpenMode.ForWrite)
                'e modelspace em escrita
                Dim model As BlockTableRecord = bt(BlockTableRecord.ModelSpace).GetObject(OpenMode.ForWrite)

                Dim totalDias As Integer = diaFinal.Subtract(diaInicial).TotalDays

                'desenhar eixo x e y com base nas datas
                'desenhar exo x
                Dim layerEixoXNome As String = "EixoGrafico"
                CriarLayer(layerEixoXNome, "Continuous", 0)
                Dim eixoXPoly As New Polyline With {.Layer = layerEixoXNome, .ConstantWidth = 5}

                distorcaoX = 500 ' escala do eixo x
                'maior valor do dado selecioando
                Dim maiorValorY = listaDados.Max(Function(i) i.Dados)

                'intervalo dos dados proporcional ao numero maximo
                Dim interEixoSecunY As Integer = 10 ^ Math.Round(Math.Log10(dadoMax)) / 10
                If interEixoSecunY < 1 Then interEixoSecunY = 1

                'valor arrendodado do ultimo eixo
                ultimoEixoY = Math.Ceiling((dadoMax + interEixoSecunY) / interEixoSecunY) * interEixoSecunY

                'valor para manter a altura dos eixos em 30000
                distorcaoY = 100000 / ultimoEixoY

                'adiciona os pontos 0,0 e datafinal,0
                eixoXPoly.AddVertexAt(eixoXPoly.NumberOfVertices, New Point2d(0, 0), 0, 0, 0)
                eixoXPoly.AddVertexAt(eixoXPoly.NumberOfVertices, New Point2d(totalDias * distorcaoX, 0), 0, 0, 0)

                'adiciona a polyline ao desenho
                AddToModelSpace(eixoXPoly, db)

                'desenhar eixo y
                Dim layerEixoYNome As String = "Eixo Grafico"
                CriarLayer(layerEixoXNome, "Continuous", 0)

                Dim eixoYPoly As New Polyline With {.Layer = layerEixoXNome}
                'adiciona os pontos 0,0 e datafinal,0
                eixoYPoly.AddVertexAt(eixoYPoly.NumberOfVertices, New Point2d(0, 0), 0, 0, 0)
                eixoYPoly.AddVertexAt(eixoYPoly.NumberOfVertices, New Point2d(0, ultimoEixoY * distorcaoY), 0, 0, 0)

                AddToModelSpace(eixoYPoly, db)

                'desenhar linha tracejada dos valores de y e texto valores
                Dim layerEixosSecunY As String = "Eixos Secundarios"
                Dim layerValoresY As String = "Valores Y"

                CriarLayer(layerEixosSecunY, "DASHED2", 250)
                CriarLayer(layerValoresY, "Continuous", 0)

                'desenhar cada eixo 
                For i = interEixoSecunY To ultimoEixoY Step interEixoSecunY
                    Dim eixoSecun As New Polyline With {.Layer = layerEixosSecunY,
                    .LinetypeScale = 1000, .ConstantWidth = 5}

                    'adicionar as linhas horizontais 
                    eixoSecun.AddVertexAt(eixoSecun.NumberOfVertices, New Point2d(0, i * distorcaoY), 0, 0, 0)
                    eixoSecun.AddVertexAt(eixoSecun.NumberOfVertices, New Point2d(totalDias * distorcaoX, i * distorcaoY), 0, 0, 0)

                    AddToModelSpace(eixoSecun, db)

                    'texto valor eixo y
                    Dim nfi As NumberFormatInfo = CType(CultureInfo.InvariantCulture.NumberFormat.Clone(), NumberFormatInfo)
                    nfi.NumberGroupSeparator = "."
                    Dim valoresYTextoE As New DBText With
                    {.Height = 250,
                    .TextString = i.ToString("N0", nfi),
                    .VerticalMode = TextVerticalMode.TextVerticalMid,
                    .HorizontalMode = TextHorizontalMode.TextRight,
                    .AlignmentPoint = New Point3d(-400, i * distorcaoY, 0),
                    .Layer = layerValoresY
                    }

                    'adicona ao database e a transaçção
                    AddToModelSpace(valoresYTextoE, db)
                    'por alguma razao precisa disso para ficar correto na tela durante a execucao
                    valoresYTextoE.AdjustAlignment(db)

                    Dim valoresYTextoD As New DBText With
                     {.Height = 250,
                     .TextString = i.ToString("N0", nfi),
                     .VerticalMode = TextVerticalMode.TextVerticalMid,
                     .HorizontalMode = TextHorizontalMode.TextLeft,
                     .AlignmentPoint = New Point3d(totalDias * distorcaoX + 400, i * distorcaoY, 0),
                     .Layer = layerValoresY
                     }

                    'adicona ao database e a transaçção
                    AddToModelSpace(valoresYTextoD, db)
                    'por alguma razao precisa disso para ficar correto na tela durante a execucao
                    valoresYTextoD.AdjustAlignment(db)

                Next

                'desenhar tick dos valores de x e texto com as datas
                Dim layerValoresX As String = "Valores X"
                CriarLayer(layerValoresX, "Continuous", 0)

                Dim layerTickEixoX As String = "Tick Eixo X"
                CriarLayer(layerTickEixoX, "Continuous", 250)

                'percorre os dias
                Dim dia As Date = diaInicial

                While dia <= diaFinal
                    Dim countDia As Integer = (dia - (diaInicial)).TotalDays

                    Dim tickEixoX As New Polyline With {.Layer = layerTickEixoX}
                    'adicionar as linhas horizontais 
                    tickEixoX.AddVertexAt(tickEixoX.NumberOfVertices, New Point2d((countDia) * distorcaoX, 0), 0, 0, 0)
                    tickEixoX.AddVertexAt(tickEixoX.NumberOfVertices, New Point2d((countDia) * distorcaoX, -300), 0, 0, 0)

                    AddToModelSpace(tickEixoX, db)

                    'texto valor eixo y
                    Dim dataAtual As DateTime = dia
                    Dim dataFormatada As String = Format(dataAtual, "dd-MMM") ' data no formatao 01-jan

                    Dim valoresXTexto As New DBText With
                    {.Height = 250,
                    .TextString = dataFormatada,
                    .VerticalMode = TextVerticalMode.TextVerticalMid,
                    .HorizontalMode = TextHorizontalMode.TextRight,
                    .AlignmentPoint = New Point3d(countDia * distorcaoX, -500, 0),
                    .Layer = "Valores X",
                    .Rotation = 45
                    }

                    'adicona ao database e a transaçção
                    AddToModelSpace(valoresXTexto, db)

                    valoresXTexto.AdjustAlignment(db)
                    dia = dia.AddDays(1)
                End While

                'nome da divisão escolhida
                Dim nomeDivisao As String = ""
                Select Case tipoDivisao
                    Case "Por Estado"
                        nomeDivisao = ComboBox_Estados.SelectedItem
                    Case "Por Município"
                        nomeDivisao = ComboBox_Municipios.SelectedItem
                    Case "Brasil"
                        nomeDivisao = "Brasil"
                End Select

                ''texto com a data atual
                Dim tamanhoTexto As Integer
                If dadoMax * distorcaoY / 8 < (diaFinal - diaInicial).TotalDays / 2 * distorcaoX / 20 Then
                    tamanhoTexto = dadoMax * distorcaoY / 8
                Else
                    tamanhoTexto = (diaFinal - diaInicial).TotalDays / 2 * distorcaoX / 20
                End If

                'titulo do grafico
                Dim tituloTexto As New DBText With
                    {.Height = tamanhoTexto,
                    .TextString = nomeDivisao & " : " & tipoDados,
                    .Position = New Point3d(totalDias / 2 * distorcaoX, ultimoEixoY * distorcaoY + tamanhoTexto * 5, 0),
                    .Layer = layerValoresY
                    }
                AddToModelSpace(tituloTexto, db)
                tituloTexto.AdjustAlignment(db)

                'zoom no grafico desenhado
                ZoomView(New Point2d(-5000, -5000), New Point2d((totalDias) * distorcaoX + 5000, ultimoEixoY * distorcaoY + tamanhoTexto * 8))

                'confirma as alterações na transação
                tr.Commit()
            End Using
        Catch
        End Try
    End Sub
    Private Sub Plotar()
        Dim doc As Document = acApp.DocumentManager.MdiActiveDocument
        Dim db As Database = doc.Database

        Using tr = doc.TransactionManager.StartTransaction

            'tabela de bloco em escrita
            Dim bt As BlockTable = db.BlockTableId.GetObject(OpenMode.ForWrite)
            'e modelspace em escrita
            Dim model As BlockTableRecord = bt(BlockTableRecord.ModelSpace).GetObject(OpenMode.ForWrite)

            'apagar graficos criados anteriormente
            ApagarEntities()
            'desenha os eixos principais e secundarios e os textos
            DesenharGráfico()

            ''texto com a data atual
            Dim tamanhoTexto As Integer
            If dadoMax * distorcaoY / 4 < (diaFinal - diaInicial).TotalDays / 2 * distorcaoX / 10 Then
                tamanhoTexto = dadoMax * distorcaoY / 8
            Else
                tamanhoTexto = (diaFinal - diaInicial).TotalDays / 2 * distorcaoX / 20
            End If

            Dim textoData As New DBText With {
                .Height = tamanhoTexto,
                .Position = New Point3d((diaFinal - diaInicial).TotalDays * distorcaoX / 2, ultimoEixoY * distorcaoY + tamanhoTexto * 3, 0)
            }

            textoData.SetDatabaseDefaults(db)
            AddToModelSpace(textoData, db)
            textoData.AdjustAlignment(db)

            'texto com o dado atual
            Dim textoDado As New DBText With {
                .Height = tamanhoTexto * 1.5,
                .Position = New Point3d((diaFinal - diaInicial).TotalDays * distorcaoX / 2, ultimoEixoY * distorcaoY + tamanhoTexto * 1, 0)
            }

            textoDado.SetDatabaseDefaults(db)
            AddToModelSpace(textoDado, db)
            textoDado.AdjustAlignment(db)

            'criar polyline dos dados
            CriarLayer("Dados", "Continuous", 0)
            Dim polyDados As New Polyline With {.Layer = "Dados", .ConstantWidth = 5}
            AddToModelSpace(polyDados, db)

            'criar polyline media movel
            CriarLayer("MediaMovel", "Continuous", 10)
            Dim polymMedia As New Polyline With {.Layer = "MediaMovel", .ConstantWidth = 10}
            AddToModelSpace(polymMedia, db)

            'Plotar pontos
            Dim intDiasInicial As Integer = 0
            Dim intDiasFinal As Integer = diaFinal.Subtract(diaInicial).TotalDays

            'percorre os dias
            Dim dia As Date = diaInicial
            While dia <= diaFinal

                'altera o valor do texto para a data atual
                textoData.TextString = dia
                Dim countDia As Integer = (dia - (diaInicial)).TotalDays
                Try
                    'pega o valor na data
                    Dim dadoAtual As Integer = listaDados.Find(Function(x) x.Data = dia).Dados

                    'apenas media para esses casos
                    If tipoDados = "Casos Novos" Or tipoDados = "Óbitos Novos" Then
                        Dim soma As Integer = 0
                        Dim media As Double = 0
                        Try
                            For i = 0 To 6
                                Dim diaIMedia As Date = dia.AddDays(-i)
                                soma += listaDados.Find(Function(x) x.Data = diaIMedia).Dados
                            Next

                            media = soma / 7

                            polymMedia.AddVertexAt(polymMedia.NumberOfVertices, New Point2d((countDia) * distorcaoX, media * distorcaoY), 0, 0, 0)
                            polymMedia.ConstantWidth = 10
                        Catch
                        End Try
                    Else
                        polymMedia.Erase()
                    End If

                    'formatar o valor do dado com separador de milhar
                    Dim nfi As NumberFormatInfo = CType(CultureInfo.InvariantCulture.NumberFormat.Clone(), NumberFormatInfo)
                    nfi.NumberGroupSeparator = "."
                    'mudando Data do texto conforme data atual
                    'conveter de dias corridos para dd/MM/aa
                    textoDado.TextString = dadoAtual.ToString("N0", nfi)

                    'adiciona o ponto referente ao valor do dado na polyline
                    polyDados.AddVertexAt(polyDados.NumberOfVertices, New Point2d((countDia) * distorcaoX, dadoAtual * distorcaoY), 0, 0, 0)
                    polyDados.ConstantWidth = 5

                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
                PausaAtualiza(10000 / listaDados.Count) '10000
                dia = dia.AddDays(1)

            End While
            tr.Commit()
        End Using
    End Sub
    Public Sub ZoomView(min2d As Point2d, max2d As Point2d)

        Dim doc As Document = acApp.DocumentManager.MdiActiveDocument
        Dim db As Database = doc.Database
        Dim ed = doc.Editor

        Dim view As New ViewTableRecord With {
            .CenterPoint = min2d + ((max2d - min2d) / 2.0),
            .Height = max2d.Y - min2d.Y,
            .Width = max2d.X - min2d.X
        }

        ed.SetCurrentView(view)
    End Sub
    Private Sub AddToModelSpace(ent As Entity, db As Database)
        Using tr As Transaction = db.TransactionManager.StartTransaction()
            Dim bt As BlockTable = CType(tr.GetObject(db.BlockTableId, OpenMode.ForRead), BlockTable)
            Dim modelSpace As BlockTableRecord = TryCast(tr.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForWrite), BlockTableRecord)

            modelSpace.AppendEntity(ent)
            tr.AddNewlyCreatedDBObject(ent, True)
            tr.Commit()
            objetosCriados.Add(ent.ObjectId)
        End Using
    End Sub
    Private Sub CriarLayer(layerNome As String, lineTypeNome As String, indexColor As Integer)

        Dim doc As Document = Application.DocumentManager.MdiActiveDocument
        Dim db As Database = doc.Database

        Using tr As Transaction = db.TransactionManager.StartTransaction()

            'abre como leitura 
            Dim layTable As LayerTable = tr.GetObject(db.LayerTableId, OpenMode.ForWrite)

            Dim layTableRec As New LayerTableRecord With {
                .Name = layerNome
            }
            'se o layer nao existe cria um
            If layTable.Has(layTableRec.Name) = False Then
                '' adicionar a tabela de layers e a transação
                layTable.Add(layTableRec)
                tr.AddNewlyCreatedDBObject(layTableRec, True)
            Else
                layTableRec = tr.GetObject(layTable(layerNome), OpenMode.ForWrite)
            End If

            'carregar linetype , verifica se ja esta carregado
            Dim lineTypeTable = TryCast(tr.GetObject(db.LinetypeTableId, OpenMode.ForRead), LinetypeTable)
            If Not lineTypeTable.Has(lineTypeNome) Then
                db.LoadLineTypeFile(lineTypeNome, "acad.lin")
            End If

            Dim cor As Color
            cor = Color.FromColorIndex(ColorMethod.ByAci, indexColor)

            'muda o linetype
            layTableRec.LinetypeObjectId = lineTypeTable(lineTypeNome)
            'altera a cor
            layTableRec.Color = cor

            tr.Commit()
        End Using
    End Sub
    Private Sub PausaAtualiza(tempo As Integer)

        'fazer uma pausa para poder visualizar os dados sendo lançados
        Const UmSeg As Double = 1.0# / (1440.0# * 60.0#)

        Dim dblEsperarAte As Date
        Now.AddSeconds(UmSeg)
        dblEsperarAte = Now.AddSeconds(UmSeg).AddSeconds(tempo / 1000)
        Do Until Now > dblEsperarAte
            'atualiza a tela com as alterações
            doc.TransactionManager.QueueForGraphicsFlush()
            doc.TransactionManager.FlushGraphics()
            ed.UpdateScreen()
        Loop
    End Sub
    Public Sub ApagarEntities()
        Using tr As Transaction = db.TransactionManager.StartTransaction()
            'percorre a colecao de entidades criadas
            For Each entId As ObjectId In objetosCriados
                Try
                    'pega a entidade como escrita
                    Dim ent As DBObject = tr.GetObject(entId, OpenMode.ForWrite)
                    'apaga a entidade
                    ent.Erase()
                Catch
                    'se não encontrar a entidade remove da colecao
                    objetosCriados.Remove(entId)
                End Try
            Next
            tr.Commit()

            objetosCriados.Clear()
        End Using
    End Sub
    Private Sub Formulario_CoronaCad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'inicia com os botoes selecionados
        RadioButton_CasosNovos.Checked = True
        RadioButton_Brasil.Checked = True
        'inicia com o combobox desabilitado
        ComboBox_Municipios.Enabled = False
        'inicia com o combobox desabilitado
        ComboBox_Estados.Enabled = False
        'esconder progress bar
        ProgressBar.Hide()
    End Sub
    Private Sub Abrir_Click(sender As Object, e As EventArgs) Handles Abrir.Click
        Dim caminho As DialogResult

        'dialogo com opcao de selecionar varios arquivos e filtro 
        Dim abrirComo As New OpenFileDialog() With {
        .Multiselect = True,
        .Title = "Abrir como",
        .Filter = "Arquivos de texto (*.csv)|*.csv"
        }

        caminho = abrirComo.ShowDialog
        listaArquivos = abrirComo.FileNames

        If listaArquivos.Length > 0 Then
            'coloca o caminho do arqvuio no textbox
            TextBox_Caminho.Text = String.Join(", ", listaArquivos)
        End If
    End Sub
    Private Sub TextBox_Caminho_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Caminho.TextChanged
        'disparado quando ha alguma alteracao na caixa de texto 
        Try
            Dim linha As String

            'mostra progress bar
            ProgressBar.Show()

            For Each arquivo In listaArquivos
                ProgressBar.Value = 0

                'abrir arquivo
                Dim fs = New FileStream(arquivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Using sr = New StreamReader(fs)
                    ' os nomes dos municipios e estados diponiveis
                    'ler uma vez o arquivo para pular a primeira linha
                    'preencher os dados do formulario
                    sr.ReadLine()
                    Do
                        linha = sr.ReadLine()
                        If (linha IsNot Nothing) Then
                            'ler cada linha, separa por ";" e adicionar as colunas a listas de municipios e estados
                            Dim itensLinha() = linha.Split(";")
                            Dim municipioLinha As String = itensLinha(2)
                            Dim estadoLinha As String = itensLinha(1)
                            If Not listaMunicipios.Contains(municipioLinha) And municipioLinha <> "" Then
                                listaMunicipios.Add(municipioLinha)
                            End If
                            If Not listaEstados.Contains(estadoLinha) And estadoLinha <> "" Then
                                listaEstados.Add(estadoLinha)
                            End If
                        End If
                        ProgressBar.Value = sr.BaseStream.Position / sr.BaseStream.Length * 100
                    Loop Until linha Is Nothing
                    sr.Close()
                End Using
            Next

            ProgressBar.Hide()

            'coloca em ordem alfabetica
            listaMunicipios.Sort()
            listaEstados.Sort()

            'adiciona a lista de municipios e estados aos comboboxs
            ComboBox_Municipios.Items.AddRange(listaMunicipios.ToArray)
            ComboBox_Estados.Items.AddRange(listaEstados.ToArray)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub Button_Gerar_Click(sender As Object, e As EventArgs) Handles Button_Gerar.Click
        'disparado ao clicar no botao gerar
        If RadioButton_Estado.Checked And ComboBox_Estados.SelectedItem = "" Then
            MsgBox("O estado não foi selecionado.")
            Return
        End If
        If RadioButton_municipio.Checked And ComboBox_Municipios.SelectedItem = "" Then
            MsgBox("O município não foi selecionado.")
            Return
        End If
        GerarDados()
        Plotar()
        Me.Hide()
    End Sub
    Private Sub Button_Cancelar_Click(sender As Object, e As EventArgs) Handles Button_Cancelar.Click
        'disparado ao clicar no botao cancelar
        Me.Hide()
    End Sub
    Private Sub Button_Limpar_Click_(sender As Object, e As EventArgs) Handles Button_Limpar.Click
        'disparado ao clicar no botao limpar
        ApagarEntities()
        PausaAtualiza(1)
    End Sub
    Private Sub RadioButton_municipio_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton_municipio.CheckedChanged
        'disparado ao selecionar o radiobutton municipio
        'o botao dos municipoios foi marcado ou desmarcado muda o combobox
        If ComboBox_Municipios.Enabled Then
            ComboBox_Municipios.Enabled = False
        Else
            ComboBox_Municipios.Enabled = True
        End If
    End Sub
    Private Sub RadioButton_Estado_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton_Estado.CheckedChanged
        'disparado ao selecionar o radiobutton estado
        'o botao dos estados foi marcado ou desmarcado muda o combobox
        If ComboBox_Estados.Enabled Then
            ComboBox_Estados.Enabled = False
        Else
            ComboBox_Estados.Enabled = True
        End If
    End Sub
End Class