Imports MySql.Data.MySqlClient
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = "Credit Reports Checker"
        Label2.Text = "Enter Credit num: "
        Button1.Text = "Show Table"
        Label3.Text = "Add/Delete:"
        Button2.Text = "Filter!"
        Button3.Text = "Update it!"
        Label4.Text = "Updated with insertion"

        TextBox2.Hide()
        Button3.Hide()
        Label3.Hide()
        ComboBox1.Hide()
        Label4.Hide()

        ComboBox1.Items.Add("Credit")
        ComboBox1.Items.Add("Debit")
        Dim lastrow As Integer = DataGridView1.Rows.Count - 1
        If lastrow >= 0 Then
            Dim col1 As String = DataGridView1.Rows(lastrow).Cells("Crednum").Value.ToString()

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox2.Hide()
        Button3.Hide()
        Label3.Hide()
        ComboBox1.Hide()
        Label4.Hide()
        TextBox1.Clear()
        TextBox2.Clear()
        ComboBox1.Text = ""
        'Button2.Show()
        Try
            Dim con As New MySqlConnection
            Dim sda As New MySqlDataAdapter
            Dim dt As New DataTable
            Dim bs As New BindingSource

            con.ConnectionString = "server=localhost;userid=root;password=dexter;database=datatransfer"
            Dim st As String = "Select * from cred_data"
            Dim cd As MySqlCommand
            cd = New MySqlCommand(st, con)
            sda.SelectCommand = cd
            sda.Fill(dt)
            bs.DataSource = dt
            DataGridView1.DataSource = bs
            sda.Update(dt)

        Catch ex As Exception
            MessageBox.Show("Record not found")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text.Equals("") Then
            MessageBox.Show("Enter any Credit card number")
        Else


            Dim con As New MySqlConnection
            Dim sda As New MySqlDataAdapter
            Dim dt As New DataTable
            Dim bs As New BindingSource
            Try

                con.ConnectionString = "server=localhost;userid=root;password=dexter;database=datatransfer"
                Dim st As String = "Select * from cred_data where crednum='" & TextBox1.Text & "'"
                Dim cont As String = "server=localhost;userid=root;password=dexter;database=datatransfer"

                Dim sqlQuery As String = "SELECT COUNT(*) FROM cred_data where crednum='" & TextBox1.Text & "'"
                Dim rowCount As Integer = 0
                Using connection As New MySqlConnection(cont)
                    connection.Open()
                    Using command As New MySqlCommand(sqlQuery, connection)
                        ' Execute the query and store the result in rowCount
                        rowCount = CInt(command.ExecuteScalar())
                    End Using
                End Using

                If rowCount <> 0 Then

                    Dim cd As MySqlCommand
                    cd = New MySqlCommand(st, con)
                    sda.SelectCommand = cd
                    sda.Fill(dt)
                    bs.DataSource = dt
                    DataGridView1.DataSource = bs
                    sda.Update(dt)

                    Label3.Show()
                    TextBox2.Show()
                    ComboBox1.Show()
                    Button3.Show()
                Else
                    MessageBox.Show("Record not found")
                End If
            Catch ex As Exception
                MessageBox.Show("Chceck the records again")
            End Try
        End If


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Dim nu As Integer
        Dim cVal As Integer
        Dim dval As Integer
        Dim out As Integer
        Dim og As Integer
        Dim reme As Integer
        Dim datet As DateTime = DateTime.Today
        datet = datet.ToString("yyyy-MM-dd")
        ' MessageBox.Show(datet)
        Dim val As Integer = Convert.ToInt32(TextBox2.Text)
        If DataGridView1.Rows.Count > 0 Then
            Dim lastr As Integer = DataGridView1.Rows.Count - 2
            Dim lastrow As DataGridViewRow = DataGridView1.Rows(lastr)
            'nu = Convert.ToInt32(lastrow.Cells("Num").Value)
            cVal = Convert.ToInt32(lastrow.Cells("Credit").Value)
            dval = Convert.ToInt32(lastrow.Cells("Debit").Value)
            out = Convert.ToInt32(lastrow.Cells("Outstanding").Value)
            reme = Convert.ToInt32(lastrow.Cells("Remaining").Value)
            og = Convert.ToInt32(lastrow.Cells("Avbl_OG").Value)
        End If
        'insert into cred_data values(2,'3009','2024-01-03',9525.3,0,100,30807.96,7405.67)
        If ComboBox1.Text.Equals("Credit") Then
            Dim con As New MySqlConnection
            con.ConnectionString = "server=localhost;userid=root;password=dexter;database=datatransfer"
            Dim cont As String = "server=localhost;userid=root;password=dexter;database=datatransfer"
            Try
                '
                Dim sqlQuery As String = "SELECT COUNT(*) FROM cred_data"

                ' Variable to store the count
                Dim rowCount As Integer = 0

                ' Create a SqlConnection
                Using connection As New MySqlConnection(cont)

                    ' Open the connection
                    connection.Open()
                    ' Create a SqlCommand
                    Using command As New MySqlCommand(sqlQuery, connection)
                        ' Execute the query and store the result in rowCount
                        rowCount = CInt(command.ExecuteScalar())
                    End Using
                End Using
                '
                Dim st As String = "insert into cred_data values(" & (rowCount + 1) & ",'" & TextBox1.Text & "','" & datet & "'," & (out - val) & ",0," & val & "," & (reme + val) & "," & (og + val) & ")"
                Dim cmd As New MySqlCommand
                cmd = New MySqlCommand(st, con)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()



                Label4.Show()
                MessageBox.Show("Successfully updated!!!")
            Catch ex As Exception
                MessageBox.Show("Check again!!")
            End Try

        ElseIf ComboBox1.Text.Equals("Debit") Then
            Dim con As New MySqlConnection
            con.ConnectionString = "server=localhost;userid=root;password=dexter;database=datatransfer"
            Dim cont As String = "server=localhost;userid=root;password=dexter;database=datatransfer"
            Try
                '
                Dim sqlQuery As String = "SELECT COUNT(*) FROM cred_data"

                ' Variable to store the count
                Dim rowCount As Integer = 0

                ' Create a SqlConnection
                Using connection As New MySqlConnection(cont)

                    ' Open the connection
                    connection.Open()
                    ' Create a SqlCommand
                    Using command As New MySqlCommand(sqlQuery, connection)
                        ' Execute the query and store the result in rowCount
                        rowCount = CInt(command.ExecuteScalar())
                    End Using
                End Using
                '
                Dim st As String = "insert into cred_data values(" & (rowCount + 1) & ",'" & TextBox1.Text & "','" & datet & "'," & (out + val) & "," & val & ",0," & (reme - val) & "," & (og - val) & ")"
                Dim cmd As New MySqlCommand
                cmd = New MySqlCommand(st, con)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
                Label4.Show()
                MessageBox.Show("Successfully updated!!!")

            Catch ex As Exception
                MessageBox.Show("Error Occurred")
            End Try

        End If

    End Sub
End Class
