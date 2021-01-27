Class MainWindow
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        If name.Text = "" Or number.Text = "" Then
            MsgBox("姓名学号不能为空")
            Exit Sub
        End If
        SetKeyValue(name.Text, "学号", number.Text, inipath)
        If wuli.Text <> "0" Then SetKeyValue(name.Text, "物理", wuli.Text, inipath)
        If huaxue.Text <> "0" Then SetKeyValue(name.Text, "化学", huaxue.Text, inipath)
        If shengwu.Text <> "0" Then SetKeyValue(name.Text, "生物", shengwu.Text, inipath)
        If zhengzhi.Text <> "0" Then SetKeyValue(name.Text, "政治", zhengzhi.Text, inipath)
        If lishi.Text <> "0" Then SetKeyValue(name.Text, "历史", lishi.Text, inipath)
        If dili.Text <> "0" Then SetKeyValue(name.Text, "地理", dili.Text, inipath)
        If jishu.Text <> "0" Then SetKeyValue(name.Text, "技术", jishu.Text, inipath)
        If yingyu.Text <> "0" Then SetKeyValue(name.Text, "英语", yingyu.Text, inipath)
        MessageBox.Show("保存成功", "", MessageBoxButton.OK, MessageBoxImage.Information)
        wuli.Text = "0"
        huaxue.Text = "0"
        shengwu.Text = "0"
        zhengzhi.Text = "0"
        lishi.Text = "0"
        dili.Text = "0"
        jishu.Text = "0"
        yingyu.Text = "0"
        name.Text = ""
        number.Text = ""
    End Sub

    Private Sub Window_Closing(sender As Object, e As ComponentModel.CancelEventArgs)
        e.Cancel = True
    End Sub

    Private Sub TextBlock_MouseUp(sender As Object, e As MouseButtonEventArgs)
        Process.Start("https://cx.zjzs.net/exam/XYKS202101CJCXYM/")
    End Sub
End Class
