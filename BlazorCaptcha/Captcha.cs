using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace BlazorCaptcha
{
    public class Captcha : ComponentBase
    {

        [Parameter]
        public string CaptchaWord { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnRefresh { get; set; }



        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            int width = 170;
            int height = 40;
            var fontFamilies = new string[] { "Courier", "Arial", "Verdana", "Times New Roman" };
            var rnd = new Random();

            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);

            g.Clear(Color.FromArgb(rnd.Next(100, 256), rnd.Next(100, 256), rnd.Next(100, 256)));

            var foreColor = Color.LightGray;
            var pen = new Pen(foreColor, 2.0f);
            g.DrawRectangle(pen, 0, 0, width, height);

            float x = 2;

            foreach (char c in CaptchaWord)
            {
                var family = fontFamilies[rnd.Next(0, fontFamilies.Length)];
                var fontcolor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                var font = new Font(family, rnd.Next(height / 2, (height / 2) + (height / 4)), FontStyle.Bold);

                SizeF textSize = g.MeasureString(c.ToString(), font);

                var angle = rnd.Next(-20, 20);
                var y = ((height - textSize.Height) / 2);

                g.TranslateTransform(x, y);
                g.RotateTransform(angle);

                g.DrawString(c.ToString(), font, new SolidBrush(fontcolor), 0, 0);

                g.ResetTransform();

                x += textSize.Width;
            }

            foreColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

            pen = new Pen(foreColor, 0.8f);
            g.DrawEllipse(pen, rnd.Next(-width, width), rnd.Next(-height, height), width, height);
            g.DrawLine(pen, 0, rnd.Next(0, height), width, rnd.Next(0, height));
            g.DrawLine(pen, 0, rnd.Next(0, height), width, rnd.Next(0, height));


            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Gif);
            string imageBase64Data = Convert.ToBase64String(ms.ToArray());
            var img = string.Format("data:image/gif;base64,{0}", imageBase64Data);
            bmp.Dispose();


            var seq = 0;
            builder.OpenElement(++seq, "div");
            builder.OpenElement(++seq, "img");
            builder.AddAttribute(++seq, "src", img);
            builder.CloseElement(); // img
            //<button class="btn btn-sm width-min btn-success">OK</button>
            builder.OpenElement(++seq, "button");
            builder.AddAttribute(++seq, "class", "btn btn-sm btn-refresh");
            builder.AddAttribute(++seq, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnRefresh));
            builder.OpenElement(++seq, "span");
            builder.AddAttribute(++seq, "class", "glyphicon glyphicon-refresh");
            builder.CloseElement(); // span
            builder.CloseElement(); //button
            builder.CloseElement();


            base.BuildRenderTree(builder);
        }

    }
}
    

