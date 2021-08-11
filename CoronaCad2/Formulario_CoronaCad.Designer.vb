<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Formulario_CoronaCad
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label_GPS = New System.Windows.Forms.Label()
        Me.Abrir = New System.Windows.Forms.Button()
        Me.TextBox_Caminho = New System.Windows.Forms.TextBox()
        Me.RadioButton_CasosNovos = New System.Windows.Forms.RadioButton()
        Me.RadioButton_ObitosAcumulados = New System.Windows.Forms.RadioButton()
        Me.RadioButton_ObitosNovos = New System.Windows.Forms.RadioButton()
        Me.RadioButton_CasosAcumulados = New System.Windows.Forms.RadioButton()
        Me.GroupBox_divisao = New System.Windows.Forms.GroupBox()
        Me.ComboBox_Municipios = New System.Windows.Forms.ComboBox()
        Me.RadioButton_municipio = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Brasil = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Estado = New System.Windows.Forms.RadioButton()
        Me.Button_Cancelar = New System.Windows.Forms.Button()
        Me.Button_Gerar = New System.Windows.Forms.Button()
        Me.GroupBox_Dados = New System.Windows.Forms.GroupBox()
        Me.RadioButton_NovosRecuperados = New System.Windows.Forms.RadioButton()
        Me.ProgressBar = New System.Windows.Forms.ProgressBar()
        Me.Button_Limpar = New System.Windows.Forms.Button()
        Me.ComboBox_Estados = New System.Windows.Forms.ComboBox()
        Me.GroupBox_divisao.SuspendLayout()
        Me.GroupBox_Dados.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label_GPS
        '
        Me.Label_GPS.AutoSize = True
        Me.Label_GPS.Location = New System.Drawing.Point(11, 9)
        Me.Label_GPS.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label_GPS.Name = "Label_GPS"
        Me.Label_GPS.Size = New System.Drawing.Size(104, 13)
        Me.Label_GPS.TabIndex = 31
        Me.Label_GPS.Text = "Selecione o arquivo:"
        '
        'Abrir
        '
        Me.Abrir.Location = New System.Drawing.Point(457, 6)
        Me.Abrir.Margin = New System.Windows.Forms.Padding(2)
        Me.Abrir.Name = "Abrir"
        Me.Abrir.Size = New System.Drawing.Size(56, 22)
        Me.Abrir.TabIndex = 30
        Me.Abrir.Text = "Abrir"
        Me.Abrir.UseVisualStyleBackColor = True
        '
        'TextBox_Caminho
        '
        Me.TextBox_Caminho.Location = New System.Drawing.Point(119, 7)
        Me.TextBox_Caminho.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox_Caminho.Name = "TextBox_Caminho"
        Me.TextBox_Caminho.Size = New System.Drawing.Size(334, 20)
        Me.TextBox_Caminho.TabIndex = 29
        '
        'RadioButton_CasosNovos
        '
        Me.RadioButton_CasosNovos.AutoSize = True
        Me.RadioButton_CasosNovos.Location = New System.Drawing.Point(6, 19)
        Me.RadioButton_CasosNovos.Name = "RadioButton_CasosNovos"
        Me.RadioButton_CasosNovos.Size = New System.Drawing.Size(88, 17)
        Me.RadioButton_CasosNovos.TabIndex = 11
        Me.RadioButton_CasosNovos.Text = "Casos Novos"
        Me.RadioButton_CasosNovos.UseVisualStyleBackColor = True
        '
        'RadioButton_ObitosAcumulados
        '
        Me.RadioButton_ObitosAcumulados.AutoSize = True
        Me.RadioButton_ObitosAcumulados.Location = New System.Drawing.Point(6, 88)
        Me.RadioButton_ObitosAcumulados.Name = "RadioButton_ObitosAcumulados"
        Me.RadioButton_ObitosAcumulados.Size = New System.Drawing.Size(116, 17)
        Me.RadioButton_ObitosAcumulados.TabIndex = 13
        Me.RadioButton_ObitosAcumulados.TabStop = True
        Me.RadioButton_ObitosAcumulados.Text = "Óbitos Acumulados"
        Me.RadioButton_ObitosAcumulados.UseVisualStyleBackColor = True
        '
        'RadioButton_ObitosNovos
        '
        Me.RadioButton_ObitosNovos.AutoSize = True
        Me.RadioButton_ObitosNovos.Location = New System.Drawing.Point(6, 65)
        Me.RadioButton_ObitosNovos.Name = "RadioButton_ObitosNovos"
        Me.RadioButton_ObitosNovos.Size = New System.Drawing.Size(89, 17)
        Me.RadioButton_ObitosNovos.TabIndex = 14
        Me.RadioButton_ObitosNovos.TabStop = True
        Me.RadioButton_ObitosNovos.Text = "Óbitos Novos"
        Me.RadioButton_ObitosNovos.UseVisualStyleBackColor = True
        '
        'RadioButton_CasosAcumulados
        '
        Me.RadioButton_CasosAcumulados.AutoSize = True
        Me.RadioButton_CasosAcumulados.Location = New System.Drawing.Point(6, 42)
        Me.RadioButton_CasosAcumulados.Name = "RadioButton_CasosAcumulados"
        Me.RadioButton_CasosAcumulados.Size = New System.Drawing.Size(115, 17)
        Me.RadioButton_CasosAcumulados.TabIndex = 12
        Me.RadioButton_CasosAcumulados.TabStop = True
        Me.RadioButton_CasosAcumulados.Text = "Casos Acumulados"
        Me.RadioButton_CasosAcumulados.UseVisualStyleBackColor = True
        '
        'GroupBox_divisao
        '
        Me.GroupBox_divisao.Controls.Add(Me.ComboBox_Estados)
        Me.GroupBox_divisao.Controls.Add(Me.ComboBox_Municipios)
        Me.GroupBox_divisao.Controls.Add(Me.RadioButton_municipio)
        Me.GroupBox_divisao.Controls.Add(Me.RadioButton_Brasil)
        Me.GroupBox_divisao.Controls.Add(Me.RadioButton_Estado)
        Me.GroupBox_divisao.Location = New System.Drawing.Point(239, 33)
        Me.GroupBox_divisao.Name = "GroupBox_divisao"
        Me.GroupBox_divisao.Size = New System.Drawing.Size(274, 140)
        Me.GroupBox_divisao.TabIndex = 33
        Me.GroupBox_divisao.TabStop = False
        Me.GroupBox_divisao.Text = "Divisão:"
        '
        'ComboBox_Municipios
        '
        Me.ComboBox_Municipios.FormattingEnabled = True
        Me.ComboBox_Municipios.Location = New System.Drawing.Point(6, 112)
        Me.ComboBox_Municipios.Name = "ComboBox_Municipios"
        Me.ComboBox_Municipios.Size = New System.Drawing.Size(262, 21)
        Me.ComboBox_Municipios.TabIndex = 16
        '
        'RadioButton_municipio
        '
        Me.RadioButton_municipio.AutoSize = True
        Me.RadioButton_municipio.Location = New System.Drawing.Point(6, 89)
        Me.RadioButton_municipio.Name = "RadioButton_municipio"
        Me.RadioButton_municipio.Size = New System.Drawing.Size(91, 17)
        Me.RadioButton_municipio.TabIndex = 15
        Me.RadioButton_municipio.Text = "Por Município"
        Me.RadioButton_municipio.UseVisualStyleBackColor = True
        '
        'RadioButton_Brasil
        '
        Me.RadioButton_Brasil.AutoSize = True
        Me.RadioButton_Brasil.Location = New System.Drawing.Point(6, 19)
        Me.RadioButton_Brasil.Name = "RadioButton_Brasil"
        Me.RadioButton_Brasil.Size = New System.Drawing.Size(50, 17)
        Me.RadioButton_Brasil.TabIndex = 11
        Me.RadioButton_Brasil.Text = "Brasil"
        Me.RadioButton_Brasil.UseVisualStyleBackColor = True
        '
        'RadioButton_Estado
        '
        Me.RadioButton_Estado.AutoSize = True
        Me.RadioButton_Estado.Location = New System.Drawing.Point(6, 42)
        Me.RadioButton_Estado.Name = "RadioButton_Estado"
        Me.RadioButton_Estado.Size = New System.Drawing.Size(77, 17)
        Me.RadioButton_Estado.TabIndex = 14
        Me.RadioButton_Estado.TabStop = True
        Me.RadioButton_Estado.Text = "Por Estado"
        Me.RadioButton_Estado.UseVisualStyleBackColor = True
        '
        'Button_Cancelar
        '
        Me.Button_Cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button_Cancelar.Location = New System.Drawing.Point(387, 188)
        Me.Button_Cancelar.Margin = New System.Windows.Forms.Padding(2)
        Me.Button_Cancelar.Name = "Button_Cancelar"
        Me.Button_Cancelar.Size = New System.Drawing.Size(60, 22)
        Me.Button_Cancelar.TabIndex = 35
        Me.Button_Cancelar.Text = "Cancelar"
        Me.Button_Cancelar.UseVisualStyleBackColor = True
        '
        'Button_Gerar
        '
        Me.Button_Gerar.Location = New System.Drawing.Point(453, 188)
        Me.Button_Gerar.Margin = New System.Windows.Forms.Padding(2)
        Me.Button_Gerar.Name = "Button_Gerar"
        Me.Button_Gerar.Size = New System.Drawing.Size(60, 22)
        Me.Button_Gerar.TabIndex = 34
        Me.Button_Gerar.Text = "Gerar"
        Me.Button_Gerar.UseVisualStyleBackColor = True
        '
        'GroupBox_Dados
        '
        Me.GroupBox_Dados.Controls.Add(Me.RadioButton_NovosRecuperados)
        Me.GroupBox_Dados.Controls.Add(Me.RadioButton_CasosNovos)
        Me.GroupBox_Dados.Controls.Add(Me.RadioButton_ObitosAcumulados)
        Me.GroupBox_Dados.Controls.Add(Me.RadioButton_ObitosNovos)
        Me.GroupBox_Dados.Controls.Add(Me.RadioButton_CasosAcumulados)
        Me.GroupBox_Dados.Location = New System.Drawing.Point(13, 32)
        Me.GroupBox_Dados.Name = "GroupBox_Dados"
        Me.GroupBox_Dados.Size = New System.Drawing.Size(220, 141)
        Me.GroupBox_Dados.TabIndex = 32
        Me.GroupBox_Dados.TabStop = False
        Me.GroupBox_Dados.Text = "Tipo de Dados:"
        '
        'RadioButton_NovosRecuperados
        '
        Me.RadioButton_NovosRecuperados.AutoSize = True
        Me.RadioButton_NovosRecuperados.Location = New System.Drawing.Point(6, 111)
        Me.RadioButton_NovosRecuperados.Name = "RadioButton_NovosRecuperados"
        Me.RadioButton_NovosRecuperados.Size = New System.Drawing.Size(123, 17)
        Me.RadioButton_NovosRecuperados.TabIndex = 15
        Me.RadioButton_NovosRecuperados.TabStop = True
        Me.RadioButton_NovosRecuperados.Text = "Novos Recuperados"
        Me.RadioButton_NovosRecuperados.UseVisualStyleBackColor = True
        '
        'ProgressBar
        '
        Me.ProgressBar.Location = New System.Drawing.Point(14, 194)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(304, 15)
        Me.ProgressBar.TabIndex = 36
        '
        'Button_Limpar
        '
        Me.Button_Limpar.Location = New System.Drawing.Point(323, 188)
        Me.Button_Limpar.Margin = New System.Windows.Forms.Padding(2)
        Me.Button_Limpar.Name = "Button_Limpar"
        Me.Button_Limpar.Size = New System.Drawing.Size(60, 22)
        Me.Button_Limpar.TabIndex = 38
        Me.Button_Limpar.Text = "Limpar"
        Me.Button_Limpar.UseVisualStyleBackColor = True
        '
        'ComboBox_Estados
        '
        Me.ComboBox_Estados.FormattingEnabled = True
        Me.ComboBox_Estados.Location = New System.Drawing.Point(6, 65)
        Me.ComboBox_Estados.Name = "ComboBox_Estados"
        Me.ComboBox_Estados.Size = New System.Drawing.Size(262, 21)
        Me.ComboBox_Estados.TabIndex = 17
        '
        'Formulario_CoronaCad
        '
        Me.AcceptButton = Me.Button_Gerar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button_Cancelar
        Me.ClientSize = New System.Drawing.Size(524, 221)
        Me.Controls.Add(Me.ProgressBar)
        Me.Controls.Add(Me.Button_Limpar)
        Me.Controls.Add(Me.Label_GPS)
        Me.Controls.Add(Me.Abrir)
        Me.Controls.Add(Me.TextBox_Caminho)
        Me.Controls.Add(Me.GroupBox_divisao)
        Me.Controls.Add(Me.Button_Cancelar)
        Me.Controls.Add(Me.Button_Gerar)
        Me.Controls.Add(Me.GroupBox_Dados)
        Me.KeyPreview = True
        Me.MaximumSize = New System.Drawing.Size(540, 260)
        Me.MinimumSize = New System.Drawing.Size(540, 260)
        Me.Name = "Formulario_CoronaCad"
        Me.Text = "Coronocad 1.0"
        Me.GroupBox_divisao.ResumeLayout(False)
        Me.GroupBox_divisao.PerformLayout()
        Me.GroupBox_Dados.ResumeLayout(False)
        Me.GroupBox_Dados.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label_GPS As Label
    Friend WithEvents Abrir As Button
    Friend WithEvents TextBox_Caminho As TextBox
    Friend WithEvents RadioButton_CasosNovos As RadioButton
    Friend WithEvents RadioButton_ObitosAcumulados As RadioButton
    Friend WithEvents RadioButton_ObitosNovos As RadioButton
    Friend WithEvents RadioButton_CasosAcumulados As RadioButton
    Friend WithEvents GroupBox_divisao As GroupBox
    Friend WithEvents RadioButton_Brasil As RadioButton
    Friend WithEvents RadioButton_Estado As RadioButton
    Friend WithEvents Button_Cancelar As Button
    Friend WithEvents Button_Gerar As Button
    Friend WithEvents GroupBox_Dados As GroupBox
    Friend WithEvents ComboBox_Municipios As ComboBox
    Friend WithEvents RadioButton_municipio As RadioButton
    Friend WithEvents RadioButton_NovosRecuperados As RadioButton
    Friend WithEvents ProgressBar As ProgressBar
    Friend WithEvents Button_Limpar As Button
    Friend WithEvents ComboBox_Estados As ComboBox
End Class
