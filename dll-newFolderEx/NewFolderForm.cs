using System;
using System.Collections;
using System.Windows.Forms;

namespace dll_newFolderEx
{
    public partial class NewFolderForm : Form
    {
        string[] files;
        public NewFolderForm(ArrayList args)
        {
            if (args.Count < 1) return;

            files = (string[])args.ToArray(typeof(string));
            InitializeComponent();
            try
            {
                tb1.Text = Find_the_same();
                tb1.Focus();
                tb1.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }


        string Find_the_same()
        {
            string[] filenames = new string[files.Length];
            Array.Copy(files, filenames, files.Length);
            
            for (int i = 0; i < filenames.Length; i++)
            {
                filenames[i] = System.IO.Path.GetFileNameWithoutExtension(filenames[i]);
            }

            // 确定最短的文件名长度
            int minLength = 0;
            foreach (var f in filenames)
            {
                minLength = Math.Min(f.Length, minLength);
            }

            if (filenames.Length < 2)
            {
                //只有一个文件，返回文件名
                return filenames[0];
            }
            else
            {
                int[] ptr = new int[filenames.Length];
                for (int i = 0; i < ptr.Length; i++) { ptr[i] = 0; }
                int pos = 0;
                for (; pos < minLength; pos++)
                {
                    bool same = true;
                    for (int j = 1; j < ptr.Length; j++)
                    {
                        if (!filenames[j].Substring(pos, 1).Equals(filenames[0].Substring(pos, 1)))
                        {
                            same = false;
                        }
                        if (!same) break;
                    }
                    if (!same) break;
                }
                if (0 == pos)
                {
                    //如果pos是0，返回第一个文件的文件名
                    return filenames[0];
                }
                else
                {
                    //TODO: 需要移除尾部的.符号
                    return filenames[0].Substring(0, pos);
                }

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (tb1.Text.Length < 1)
            {
                return;
            }
            this.Enabled = false;
            tb1.Enabled = false;
            // create folder
            string current_folder = System.IO.Path.GetDirectoryName(files[0]);
            string new_folder = System.IO.Path.Combine(current_folder, tb1.Text);
            if (!System.IO.Directory.Exists(new_folder))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(new_folder);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.Close();
                }
            }
            // move files
            for (int i = 0; i < files.Length; i++)
            {
                //TODO: 需要补充文件夹操作
                string filename = System.IO.Path.GetFileName(files[i]);

                string new_filename = System.IO.Path.Combine(new_folder, filename);
                //TODO: 需要补充错误逻辑

                if (System.IO.Directory.Exists(files[i]))
                {
                    try
                    {
                        System.IO.Directory.Move(files[i], new_filename);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }
                else
                {
                    try
                    {
                        System.IO.File.Move(files[i], new_filename);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                        this.Close();
                    }
                }
            }
            this.Close();
        }

        private void tb1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
