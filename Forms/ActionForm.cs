using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Capshot
{
    public partial class ActionForm : Form
    {
        #region Variables and enums

        public enum Action
        {
            Clipboard = 1 << 0,
            Save      = 1 << 1,
            SaveAs    = 1 << 2,
            Edit      = 1 << 3
        };

        Bitmap Img = null;
        static List<Action> EnabledActions;

        static readonly SaveFileDialog sfd = new SaveFileDialog
        {
            Title = "Elija donde guardar la captura",
            Filter = "Imagen PNG|*.png|Imagen JPG|*.jpg|Imagen GIF|*.gif"
        };

        static readonly SaveFileDialog gifSfd = new SaveFileDialog {
            Title = "Elija donde guardar la grabación",
            Filter = "Imagen GIF animada|*.gif"
        };

        public static string SelectedFile;
        public static bool SelectedEdit;

        #endregion

        #region Constructors

        ActionForm(bool gif)
        {
            InitializeComponent();
            Icon = Properties.Resources.appiconsmall;

            Init(gif);
            foreach (var action in EnabledActions)
                EnableAction(action);
        }

        ActionForm(Bitmap img) : this(false)
        {
            Img = img;

            foreach (var action in EnabledActions)
                EnableAction(action);
        }

        #endregion

        #region Init

        void ActionForm_Load(object sender, EventArgs e)
        {
            var pt = Program.GetTopLeftLocationCenterMouse(Size);

            Left = pt.X;
            Top = pt.Y;
        }

        static void Init(bool gif = false)
        {
            var destinations = (Action)Settings.GetValue<int>("destinations");
            EnabledActions = new List<Action>();

            if (destinations.HasFlag(Action.Clipboard) && (!gif || IsValidGifAction(Action.Clipboard)))
                EnabledActions.Add(Action.Clipboard);

            if (destinations.HasFlag(Action.Save) && (!gif || IsValidGifAction(Action.Save)))
                EnabledActions.Add(Action.Save);

            if (destinations.HasFlag(Action.SaveAs) && (!gif || IsValidGifAction(Action.SaveAs)))
                EnabledActions.Add(Action.SaveAs);

            if (destinations.HasFlag(Action.Edit) && (!gif || IsValidGifAction(Action.Edit)))
                EnabledActions.Add(Action.Edit);
        }

        #endregion

        #region Controls enabler

        void EnableAction(Action action)
        {
            switch (action)
            {
                case Action.Clipboard:
                    clipboardB.Visible = true;
                    break;
                case Action.Save:
                    saveB.Visible = true;
                    break;
                case Action.SaveAs:
                    saveAsB.Visible = true;
                    break;
                case Action.Edit:
                    editB.Visible = true;
                    break;
            }
        }

        #endregion

        #region Action performing

        public static void PerformAction(Bitmap img)
        {
            Init();
            
            if (EnabledActions.Count == 0)
                PerformAction(img, Action.SaveAs); // Default action

            else if (EnabledActions.Count == 1)
                PerformAction(img, EnabledActions[0]);

            else
                new ActionForm(img).Show();
        }

        // Perform the desired action
        public static string PerformAction(Bitmap img, Action action)
        {
            if (img == null) // Gif
            {
                SetFileName(action); // To get file name
                return string.Empty;
            }

            switch (action)
            {
                case Action.Clipboard:
                    Clipboard.SetImage(img);
                    img.Dispose();
                    break;

                case Action.Save:
                    var file = GetAutosaveFileName();
                    img.Save(file, GetImageFormat(Path.GetExtension(file)));
                    img.Dispose();
                    return file;

                case Action.SaveAs:
                    sfd.FileName = Path.GetFileName(GetAutosaveFileName());
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        img.Save(sfd.FileName, GetImageFormat(Path.GetExtension(sfd.FileName)));
                        img.Dispose();
                        return "Archivo guardado correctamente";
                    }
                    img.Dispose();
                    return "El archivo no se ha guardado";

                case Action.Edit:
                    new EditForm(img).Show();
                    break;
            }
            GC.Collect();

            return string.Empty;
        }

        public static string GetGifSaveLocation()
        {
            Init(true);

            if (!AnyGifAction())
                SetFileName(Action.SaveAs); // Default action

            else if (EnabledActions.Count == 1)
                SetFileName(EnabledActions[0]);

            else using (var af = new ActionForm(true))
                    af.ShowDialog();

            return SelectedFile;
        }

        public static string GetGifSaveLocation(Action action)
        {
            Init(true);

            if (EnabledActions.Count == 1)
                SetFileName(action);

            return SelectedFile;
        }

        static bool AnyGifAction()
        {
            foreach (var action in EnabledActions)
                if (IsValidGifAction(action))
                    return true;

            return false;
        }

        public static void SetFileName(Action action) // We only want to set the location
        {
            SelectedEdit = false;
            SelectedFile = string.Empty;

            switch (action)
            {
                case Action.Save:
                    SelectedFile = GetAutosaveFileName(true);
                    break;

                case Action.SaveAs:
                    gifSfd.FileName = Path.GetFileName(GetAutosaveFileName(true));
                    if (gifSfd.ShowDialog() == DialogResult.OK)
                        SelectedFile = gifSfd.FileName;
                    break;

                case Action.Edit:
                    SelectedEdit = true;
                    break;
            }
        }


        static string GetAutosaveFileName(bool gif = false)
        {
            var dir = Settings.GetValue<string>("autosaveFolder");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var ext = gif ? ".gif" : Settings.GetValue<string>("autosaveExtension");
            var file = Path.Combine(dir, GetAutoFormat());

            var endf = file + ext;
            int count = 1;
            while (File.Exists(endf))
                endf = file + " (" + count++ + ")" + ext;

            return endf;
        }

        public static string GetAutoFormat()
        {
            var autosaveFormat = Settings.GetValue<string>("autosaveFormat");

            foreach (var invalidChar in Path.GetInvalidFileNameChars())
                if (autosaveFormat.Contains(invalidChar))
                    autosaveFormat = autosaveFormat.Replace(invalidChar.ToString(), "");

            // Use ? and * as temporal values
            autosaveFormat = autosaveFormat
                .Replace("{dia}", "?0:dd*")
                .Replace("{nombredia}", "?0:dddd*")
                .Replace("{mes}", "?0:MM*")
                .Replace("{nombremes}", "?0:MMMM*")
                .Replace("{año}", "?0:yyyy*")
                .Replace("{hora}", "?0:HH*")
                .Replace("{minuto}", "?0:mm*")
                .Replace("{segundo}", "?0:ss*")
                .Replace("{", "").Replace("?", "{")
                .Replace("}", "").Replace("*", "}");

            if (autosaveFormat.Length == 0)
                return "Captura";

            return string.Format(autosaveFormat, DateTime.Now);

        }

        static ImageFormat GetImageFormat(string extension) {
            switch (extension) {
                case ".jpg": return ImageFormat.Jpeg;
                case ".gif": return ImageFormat.Gif;
                case ".png":
                default: return ImageFormat.Png;
            }
        }

        static bool IsValidGifAction(Action action) {
            return action == Action.Edit || action == Action.Save || action == Action.SaveAs;
        }

        void clipboardB_Click(object sender, EventArgs e) { PerformAndClose(Action.Clipboard); }
        void saveB_Click(object sender, EventArgs e) { PerformAndClose(Action.Save); }
        void saveAsB_Click(object sender, EventArgs e) { PerformAndClose(Action.SaveAs); }
        void editB_Click(object sender, EventArgs e) { PerformAndClose(Action.Edit); }

        void PerformAndClose(Action action)
        {
            PerformAction(Img, action);
            Close();
        }

        #endregion
    }
}