using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using SharpShell.SharpContextMenu;
using System.Runtime.InteropServices;
using SharpShell.Attributes;
using System.Collections;

namespace dll_newFolderEx
{
    [ComVisible(true)]
    [COMServerAssociation(AssociationType.AllFilesAndFolders)]
    public class NewFolderExt : SharpContextMenu
    {
        protected override bool CanShowMenu()
        {
            return true;
            // throw new NotImplementedException();
        }

        protected override ContextMenuStrip CreateMenu()
        {
            var menu = new ContextMenuStrip();

            var create_folder_context_item = new ToolStripMenuItem
            {
                //TODO: 图标
                Text = Properties.Resources.context_menu_text,
                Image = Properties.Resources.icon_nf.ToBitmap()
            };
            create_folder_context_item.Click += (sender, args) => CreateFolder();
            menu.Items.Add(create_folder_context_item);
            return menu;
        }

        private void CreateFolder()
        {
            var builder = new StringBuilder();
            ArrayList pathlist = new ArrayList();
            var current_folder = System.IO.Path.GetDirectoryName(SelectedItemPaths.First());

            foreach (var filepath in SelectedItemPaths)
            {
                builder.AppendLine(filepath);
                pathlist.Add(filepath);                
            }
            NewFolderForm form = new NewFolderForm(pathlist);
            form.ShowDialog();
        }
    }
}
