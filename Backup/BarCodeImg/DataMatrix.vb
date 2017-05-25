Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Drawing.Imaging
Imports IEC16022Sharp
Imports System.IO
Imports EaseSoft.Encoder
Imports EaseSoftBarcode

Public Class DataMatrixEnc

    Dim intTitle, intBarcode, intCenter, intBoxNum, intFlowNo, intBottomValue, intBottomName, intBillNo, intQty, intModel As New Integer
    Dim pBarcode, pTitle, pFlowNo, pBoxNum, pCenter, pBottom, pBillNo, pQty, pModel As New Point
    Dim BarCodeText As String = ""
    Dim sTitle, sFDocHight, sFDocClear, sFDate, sFOrderBillNo, sFDocQty, sFUnitName, sFModel, sFBoxNum, sFlowNo, sFTradeName As String
    Dim ZoomFactor As Double = 1
    Dim offsetX, offsetY, labelWidth, labelHeight As Integer


    'Private Sub SetBarCodeSize(ByVal intSize As Integer)
    '    intBarcode = intSize '��ά���С
    'End Sub
    'Private Sub SetTitleSize(ByVal intSize As Integer)
    '    intTitle = intSize '�����������С
    'End Sub
    'Private Sub SetFlowNoSize(ByVal intSize As Integer)
    '    intFlowNo = intSize '��ˮ�������С
    'End Sub
    'Private Sub SetBoxNumSize(ByVal intSize As Integer)
    '    intBoxNum = intSize '��������С
    'End Sub
    'Private Sub SetCenterSize(ByVal intSize As Integer)
    '    intCenter = intSize '��װ���������С
    'End Sub
    'Private Sub SetBottomValueSize(ByVal intSize As Integer)
    '    intBottomValue = intSize '�ײ����ݡ�ֵ�������С
    'End Sub
    'Private Sub SetBottomNameSize(ByVal intSize As Integer)
    '    intBottomName = intSize '�ײ����ݡ����ơ������С
    'End Sub

    Public Sub New(Optional ByVal labelWidth As Integer = 400, Optional ByVal labelHeight As Integer = 240, Optional ByVal offX As Integer = 5, Optional ByVal offY As Integer = 5, Optional ByVal ZF As Double = 1.0)
        Me.labelWidth = labelWidth
        Me.labelHeight = labelHeight
        ZoomFactor = ZF
        Me.offsetX = offX
        Me.offsetY = offY
        '��ʼ����С
        intTitle = 12 * ZoomFactor
        intBarcode = 2 * ZoomFactor '1-4
        intCenter = 9 * ZoomFactor
        intBoxNum = 16 * ZoomFactor
        intFlowNo = 9 * ZoomFactor
        intBottomName = 14 * ZoomFactor
        intBottomValue = 18 * ZoomFactor
        intBillNo = 18 * ZoomFactor
        intQty = 18 * ZoomFactor
        intModel = 18 * ZoomFactor

        '��ʼ��λ��
        pBarcode = New Point((10 + Me.offsetX) * ZoomFactor, (30 + Me.offsetY) * ZoomFactor + Me.offsetY)
        pTitle = New Point((85 + Me.offsetX) * ZoomFactor, (5 + Me.offsetY) * ZoomFactor + Me.offsetY)
        pFlowNo = New Point((325 + Me.offsetX) * ZoomFactor, (5 + Me.offsetY) * ZoomFactor + Me.offsetY)
        pBoxNum = New Point((260 + Me.offsetX) * ZoomFactor, (30 + Me.offsetY) * ZoomFactor + Me.offsetY)
        pCenter = New Point((110 + Me.offsetX) * ZoomFactor, (30 + Me.offsetY) * ZoomFactor + Me.offsetY)
        pBottom = New Point((0 + Me.offsetX) * ZoomFactor, (145 + Me.offsetY) * ZoomFactor + Me.offsetY)

        pBillNo = New Point((55 + Me.offsetX) * ZoomFactor, (142 + Me.offsetY) * ZoomFactor + Me.offsetY)
        pQty = New Point((255 + Me.offsetX) * ZoomFactor, (142 + Me.offsetY) * ZoomFactor + Me.offsetY)
        pModel = New Point((55 + Me.offsetX) * ZoomFactor, (167 + Me.offsetY) * ZoomFactor + Me.offsetY)
    End Sub

    Private Sub InitSizeNLocation(ByVal offsetX As Integer, ByVal offsetY As Integer, ByVal ZF As Double)
        ZoomFactor = ZF
        Me.offsetX = offsetX
        Me.offsetY = offsetY
        '��ʼ����С
        intTitle = 12 * ZoomFactor
        intBarcode = 2 * ZoomFactor '1-4
        intCenter = 9 * ZoomFactor
        intBoxNum = 16 * ZoomFactor
        intFlowNo = 9 * ZoomFactor
        intBottomName = 14 * ZoomFactor
        intBottomValue = 18 * ZoomFactor
        intBillNo = 18 * ZoomFactor
        intQty = 18 * ZoomFactor
        intModel = 18 * ZoomFactor
        '��ʼ��λ��
        pBarcode = New Point((10 + Me.offsetX) * ZoomFactor, (30 + Me.offsetY) * ZoomFactor + Me.offsetY)
        pTitle = New Point((90 + Me.offsetX) * ZoomFactor, (5 + Me.offsetY) * ZoomFactor + Me.offsetY)
        pFlowNo = New Point((325 + Me.offsetX) * ZoomFactor, (5 + Me.offsetY) * ZoomFactor + Me.offsetY)
        pBoxNum = New Point((260 + Me.offsetX) * ZoomFactor, (30 + Me.offsetY) * ZoomFactor + Me.offsetY)
        pCenter = New Point((110 + Me.offsetX) * ZoomFactor, (30 + Me.offsetY) * ZoomFactor + Me.offsetY)
        pBottom = New Point((0 + Me.offsetX) * ZoomFactor, (145 + Me.offsetY) * ZoomFactor + Me.offsetY)

        pBillNo = New Point((55 + Me.offsetX) * ZoomFactor, (142 + Me.offsetY) * ZoomFactor + Me.offsetY)
        pQty = New Point((255 + Me.offsetX) * ZoomFactor, (142 + Me.offsetY) * ZoomFactor + Me.offsetY)
        pModel = New Point((55 + Me.offsetX) * ZoomFactor, (167 + Me.offsetY) * ZoomFactor + Me.offsetY)
    End Sub

    Public Function GenBarcodeImg(ByVal BarCodeText As String, Optional ByVal imgFormat As ImageFormat = Nothing) As Image
        If imgFormat Is Nothing Then
            imgFormat = ImageFormat.Png
        End If
        Dim bar As New DataMatrix(BarCodeText, EncodingType.Ascii)
        Dim ms As New MemoryStream()
        bar.Image.Save(ms, imgFormat)
        Dim bmp As New Bitmap(ms)
        Return (bmp)
    End Function

    Public Function GenBarcodeImg(ByVal BarCodeText As String, ByVal resizeFactor As Integer) As Image
        Dim bar As New DataMatrix(BarCodeText, EncodingType.Ascii)
        Return DMImgUtility.SimpleResizeBmp(bar.Image, resizeFactor, 0)
    End Function

    Public Function GenLableImg() As Image
        Dim bar As New DataMatrix(BarCodeText, EncodingType.Ascii)
        Dim bmp As New Bitmap(CType(labelWidth * ZoomFactor, Integer), CType(labelHeight * ZoomFactor, Integer))
        Dim g As Graphics
        g = Graphics.FromImage(bmp)
        g.FillRectangle(Brushes.White, New Rectangle(0, 0, labelWidth * ZoomFactor, labelHeight * ZoomFactor))

        g.DrawImage(DMImgUtility.SimpleResizeBmp(bar.Image, intBarcode, 0), pBarcode.X, pBarcode.Y, bar.W * intBarcode, bar.H * intBarcode)
        g.DrawString(sTitle, New Font("������", intTitle, FontStyle.Regular), Brushes.Black, pTitle.X, pTitle.Y)
        g.DrawString(sFlowNo, New Font("������", intFlowNo, FontStyle.Regular), Brushes.Black, pFlowNo.X, pFlowNo.Y)
        g.DrawString(sFBoxNum, New Font("������", intBoxNum, FontStyle.Bold), Brushes.Black, pBoxNum.X, pBoxNum.Y)

        g.DrawString("ë��/Gross(KG):", New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X, pCenter.Y)
        g.DrawString(sFDocHight, New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X + 100 * ZoomFactor, pCenter.Y)
        g.DrawString("����/Net(KG)  :", New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X, pCenter.Y + 20 * ZoomFactor)
        g.DrawString(sFDocClear, New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X + 100 * ZoomFactor, pCenter.Y + 20 * ZoomFactor)
        g.DrawString("��װ����/Date :", New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X, pCenter.Y + 40 * ZoomFactor)
        g.DrawString(sFDate, New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X + 100 * ZoomFactor, pCenter.Y + 40 * ZoomFactor)
        g.DrawString("ó������/Trade:", New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X, pCenter.Y + 60 * ZoomFactor)
        g.DrawString(sFTradeName, New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X + 100 * ZoomFactor, pCenter.Y + 60 * ZoomFactor)

        g.DrawString("Order:", New Font("����", intBottomName, FontStyle.Regular), Brushes.Black, pBottom.X, pBottom.Y)
        g.DrawString(sFOrderBillNo, New Font("����", intBottomValue, FontStyle.Regular), Brushes.Black, pBottom.X + 60 * ZoomFactor, pBottom.Y - 3 * ZoomFactor)
        g.DrawString("Model:", New Font("����", intBottomName, FontStyle.Regular), Brushes.Black, pBottom.X, pBottom.Y + 25 * ZoomFactor)
        If sFModel.Length > 25 Then
            g.DrawString(sFModel.Substring(0, 25), New Font("����", intBottomValue, FontStyle.Regular), Brushes.Black, pBottom.X + 60 * ZoomFactor, pBottom.Y + 22 * ZoomFactor)
            g.DrawString(sFModel.Substring(25, IIf(sFModel.Length > 50, 25, sFModel.Length Mod 25 - 0)), New Font("����", intBottomValue, FontStyle.Regular), Brushes.Black, pBottom.X + 60 * ZoomFactor, pBottom.Y + 47 * ZoomFactor)
        Else
            g.DrawString(sFModel, New Font("����", intBottomValue, FontStyle.Regular), Brushes.Black, pBottom.X + 60 * ZoomFactor, pBottom.Y + 22 * ZoomFactor)
        End If
        g.DrawString("Qty/P:", New Font("����", intBottomName, FontStyle.Regular), Brushes.Black, pBottom.X + 220 * ZoomFactor, pBottom.Y)
        g.DrawString(sFDocQty & sFUnitName, New Font("����", intBottomValue, FontStyle.Regular), Brushes.Black, pBottom.X + 265 * ZoomFactor, pBottom.Y - 3 * ZoomFactor)
        g.Dispose()
        'ԭ���壺Bodoni MT Condensed
        Return bmp
    End Function

    Public Function GenLableImg(ByVal BarCode As String, ByVal sTitle As String, ByVal sFDocHight As String, ByVal sFDocClear As String, ByVal sFDate As String, ByVal sFOrderBillNo As String, ByVal sFDocQty As String, ByVal sFUnitName As String, ByVal sFModel As String, ByVal sFBoxNum As String, ByVal sFlowNo As String, ByVal sFTradeName As String, Optional ByVal sFBillNo As String = "") As Image
        Dim bar As New DataMatrix(BarCode, EncodingType.Ascii)
        Dim bmp As New Bitmap(CType(labelWidth * ZoomFactor, Integer), CType(labelHeight * ZoomFactor, Integer))
        Dim g As Graphics
        g = Graphics.FromImage(bmp)
        g.FillRectangle(Brushes.White, New Rectangle(0, 0, labelWidth * ZoomFactor, labelHeight * ZoomFactor))

        g.DrawImage(DMImgUtility.SimpleResizeBmp(bar.Image, intBarcode, 0), pBarcode.X, pBarcode.Y, bar.W * intBarcode, bar.H * intBarcode)
        g.DrawString(sTitle, New Font("������", intTitle, FontStyle.Regular), Brushes.Black, pTitle.X, pTitle.Y)
        g.DrawString(sFlowNo, New Font("������", intFlowNo, FontStyle.Regular), Brushes.Black, pFlowNo.X, pFlowNo.Y)
        g.DrawString(sFBoxNum, New Font("������", intBoxNum, FontStyle.Bold), Brushes.Black, pBoxNum.X, pBoxNum.Y)

        g.DrawString("ë��/Gross(KG):", New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X, pCenter.Y)
        g.DrawString(sFDocHight, New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X + 100 * ZoomFactor, pCenter.Y)
        g.DrawString("����/Net(KG)  :", New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X, pCenter.Y + 20 * ZoomFactor)
        g.DrawString(sFDocClear, New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X + 100 * ZoomFactor, pCenter.Y + 20 * ZoomFactor)
        g.DrawString("��װ����/Date :", New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X, pCenter.Y + 40 * ZoomFactor)
        g.DrawString(sFDate, New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X + 100 * ZoomFactor, pCenter.Y + 40 * ZoomFactor)
        g.DrawString("ó������/Trade:", New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X, pCenter.Y + 60 * ZoomFactor)
        g.DrawString(sFTradeName, New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X + 100 * ZoomFactor, pCenter.Y + 60 * ZoomFactor)

        '������
        g.DrawString("Order:", New Font("����", intBottomName, FontStyle.Regular), Brushes.Black, pBottom.X, pBottom.Y)
        If sFOrderBillNo.Length > 12 Then
            intBillNo -= Math.Ceiling((sFOrderBillNo.Length - 11) / 12) * ((sFOrderBillNo.Length - 11) Mod 12)
            pBillNo.Y += Math.Ceiling((sFOrderBillNo.Length - 11) / 12) * ((sFOrderBillNo.Length - 11) Mod 12)
        Else
            intBillNo = 18 * ZoomFactor
            pBillNo = New Point((55 + Me.offsetX) * ZoomFactor, (142 + Me.offsetY) * ZoomFactor + Me.offsetY)
        End If
        g.DrawString(sFOrderBillNo, New Font("����", intBillNo, FontStyle.Regular), Brushes.Black, New RectangleF(pBillNo.X, pBillNo.Y, 162, 32))
        '����ͺ�
        g.DrawString("Model:", New Font("����", intBottomName, FontStyle.Regular), Brushes.Black, pBottom.X, pBottom.Y + 25 * ZoomFactor)
        g.DrawString(sFModel, New Font("����", intModel, FontStyle.Regular), Brushes.Black, New RectangleF(pModel.X, pModel.Y, 315, 64), New StringFormat(StringFormatFlags.LineLimit))
        '����
        g.DrawString("Qty/P:", New Font("����", intBottomName, FontStyle.Regular), Brushes.Black, pBottom.X + 200 * ZoomFactor, pBottom.Y)
        If sFDocQty.Length > 6 Then
            intQty -= Math.Ceiling((sFDocQty.Length - 5) / 6) * ((sFDocQty.Length - 5) Mod 6)
            pQty.Y += Math.Ceiling((sFDocQty.Length - 5) / 6) * ((sFDocQty.Length - 5) Mod 6)
        Else
            intQty = 18 * ZoomFactor
            pQty = New Point((255 + Me.offsetX) * ZoomFactor, (142 + Me.offsetY) * ZoomFactor + Me.offsetY)
        End If
        g.DrawString(sFDocQty & sFUnitName, New Font("����", intQty, FontStyle.Regular), Brushes.Black, pQty.X, pQty.Y)
        g.Dispose()
        'ԭ���壺Bodoni MT Condensed
        Return bmp
    End Function

    Public Function GenLableImg(ByVal BarCode As String, ByVal sTitle As String, ByVal sFDocHight As String, ByVal sFDocClear As String, ByVal sFDate As String, ByVal sFOrderBillNo As String, ByVal sFDocQty As String, ByVal sFUnitName As String, ByVal sFModel As String, ByVal sFBoxNum As String, ByVal sFlowNo As String, ByVal sFTradeName As String, ByVal sFSupplierName As String, ByVal sFCOO As String, ByVal sFItemName As String) As Image
        Dim bar As New DataMatrix(BarCode, EncodingType.Ascii)
        Dim bmp As New Bitmap(CType(labelWidth * ZoomFactor, Integer), CType(labelHeight * ZoomFactor, Integer))
        Dim g As Graphics
        g = Graphics.FromImage(bmp)
        g.FillRectangle(Brushes.White, New Rectangle(0, 0, labelWidth * ZoomFactor, labelHeight * ZoomFactor))

        g.DrawImage(DMImgUtility.SimpleResizeBmp(bar.Image, intBarcode, 0), pBarcode.X, pBarcode.Y, bar.W * intBarcode, bar.H * intBarcode)
        g.DrawString(sTitle, New Font("������", intTitle, FontStyle.Regular), Brushes.Black, pTitle.X, pTitle.Y) 'To
        g.DrawString(sFlowNo, New Font("������", intFlowNo, FontStyle.Regular), Brushes.Black, pFlowNo.X, pFlowNo.Y) '��ˮ��
        g.DrawString(sFBoxNum, New Font("������", intBoxNum, FontStyle.Bold), Brushes.Black, pBoxNum.X, pBoxNum.Y) '���


        g.DrawString("ë��/Gross(KG):", New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X, pCenter.Y)
        g.DrawString(sFDocHight, New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X + 95 * ZoomFactor, pCenter.Y)
        g.DrawString("����/Net(KG)  :", New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X, pCenter.Y + 16 * ZoomFactor)
        g.DrawString(sFDocClear, New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X + 95 * ZoomFactor, pCenter.Y + 16 * ZoomFactor)
        g.DrawString("��װ����/Date :", New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X, pCenter.Y + 32 * ZoomFactor)
        g.DrawString(sFDate, New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X + 95 * ZoomFactor, pCenter.Y + 32 * ZoomFactor)
        g.DrawString("ó������/Trade:", New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X, pCenter.Y + 48 * ZoomFactor)
        g.DrawString(sFTradeName, New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X + 95 * ZoomFactor, pCenter.Y + 48 * ZoomFactor)

        g.DrawString(sFSupplierName, New Font("������", intCenter, FontStyle.Regular), Brushes.Black, New RectangleF(pCenter.X, pCenter.Y + 64 * ZoomFactor, 245, 17))
        g.DrawString("��Ʒ/Product  :", New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X, pCenter.Y + 80 * ZoomFactor)
        g.DrawString(sFItemName, New Font("������", intCenter, FontStyle.Regular), Brushes.Black, New RectangleF(pCenter.X + 95 * ZoomFactor, pCenter.Y + 80 * ZoomFactor, 155, 17))
        g.DrawString("ԭ����/Origin :", New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X, pCenter.Y + 96 * ZoomFactor)
        g.DrawString(sFCOO, New Font("������", intCenter, FontStyle.Regular), Brushes.Black, pCenter.X + 95 * ZoomFactor, pCenter.Y + 96 * ZoomFactor)

        '������
        g.DrawString("Order:", New Font("����", intBottomName, FontStyle.Regular), Brushes.Black, pBottom.X, pBottom.Y)
        If sFOrderBillNo.Length > 12 Then
            intBillNo -= Math.Ceiling((sFOrderBillNo.Length - 11) / 12) * ((sFOrderBillNo.Length - 11) Mod 12)
            pBillNo.Y += Math.Ceiling((sFOrderBillNo.Length - 11) / 12) * ((sFOrderBillNo.Length - 11) Mod 12)
        Else
            intBillNo = 18 * ZoomFactor
            pBillNo = New Point((55 + Me.offsetX) * ZoomFactor, (142 + Me.offsetY) * ZoomFactor + Me.offsetY)
        End If
        g.DrawString(sFOrderBillNo, New Font("����", intBillNo, FontStyle.Regular), Brushes.Black, New RectangleF(pBillNo.X, pBillNo.Y, 162, 32))
        '����ͺ�
        g.DrawString("Model:", New Font("����", intBottomName, FontStyle.Regular), Brushes.Black, pBottom.X, pBottom.Y + 25 * ZoomFactor)
        g.DrawString(sFModel, New Font("����", intModel, FontStyle.Regular), Brushes.Black, New RectangleF(pModel.X, pModel.Y, 315, 64), New StringFormat(StringFormatFlags.LineLimit))
        '����
        g.DrawString("Qty/P:", New Font("����", intBottomName, FontStyle.Regular), Brushes.Black, pBottom.X + 200 * ZoomFactor, pBottom.Y)
        If sFDocQty.Length > 6 Then
            intQty -= Math.Ceiling((sFDocQty.Length - 5) / 6) * ((sFDocQty.Length - 5) Mod 6)
            pQty.Y += Math.Ceiling((sFDocQty.Length - 5) / 6) * ((sFDocQty.Length - 5) Mod 6)
        Else
            intQty = 18 * ZoomFactor
            pQty = New Point((255 + Me.offsetX) * ZoomFactor, (142 + Me.offsetY) * ZoomFactor + Me.offsetY)
        End If
        g.DrawString(sFDocQty & sFUnitName, New Font("����", intQty, FontStyle.Regular), Brushes.Black, pQty.X, pQty.Y)
        g.Dispose()
        'ԭ���壺Bodoni MT Condensed
        Return bmp
    End Function

    'If sFModel.Length > 25 Then
    '    g.DrawString(sFModel.Substring(0, 25), New Font("����", intBottomValue, FontStyle.Regular), Brushes.Black, pBottom.X + 60 * ZoomFactor, pBottom.Y + 22 * ZoomFactor)
    '    g.DrawString(sFModel.Substring(25, IIf(sFModel.Length > 50, 25, sFModel.Length Mod 25 - 0)), New Font("����", intBottomValue, FontStyle.Regular), Brushes.Black, pBottom.X + 60 * ZoomFactor, pBottom.Y + 47 * ZoomFactor)
    'Else
    '    g.DrawString(sFModel, New Font("����", intBottomValue, FontStyle.Regular), Brushes.Black, pBottom.X + 60 * ZoomFactor, pBottom.Y + 22 * ZoomFactor)
    'End If

    Public Function GenDRnoLable(ByVal FBillNo As String, Optional ByVal x As Integer = 15, Optional ByVal y As Integer = 16, Optional ByVal width As Integer = 155, Optional ByVal height As Integer = 30) As Image
        Dim ln As New LinearEncoder
        Dim easeWinControl1 As New EaseWinControl
        easeWinControl1.SymbologyID = Symbology.Code128
        easeWinControl1.BarHeight = 250
        easeWinControl1.TopComment = ""
        easeWinControl1.TextToEncode = FBillNo

        Dim pt0 As New Point(0, 0)
        Dim s As New Size(width, height)
        Dim bmp As New Bitmap(width, height)
        Dim g As Graphics
        g = Graphics.FromImage(bmp)
        g.FillRectangle(Brushes.White, New Rectangle(pt0, s))

        Dim bm As New Bitmap(s.Width + x, s.Height + y)
        easeWinControl1.DrawToBitmap(bm, New Rectangle(pt0, bm.Size))
        g.DrawImage(bm, 0, 0, New Rectangle(x, y, s.Width, s.Height), GraphicsUnit.Pixel)
        g.Dispose()
        Return bmp
    End Function
End Class
