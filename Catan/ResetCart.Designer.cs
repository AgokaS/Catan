namespace Catan
{
    partial class ResetCart
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        public void InitializeComponent()
        {
            this.Stone = new System.Windows.Forms.Button();
            this.Glay = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Wood = new System.Windows.Forms.Button();
            this.Wool = new System.Windows.Forms.Button();
            this.Whaet = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.Acept = new System.Windows.Forms.Button();
            this.HaveStone = new System.Windows.Forms.Label();
            this.ResetStone = new System.Windows.Forms.Label();
            this.HaveGlay = new System.Windows.Forms.Label();
            this.ResetGlay = new System.Windows.Forms.Label();
            this.HaveWood = new System.Windows.Forms.Label();
            this.ResetWood = new System.Windows.Forms.Label();
            this.HaveWool = new System.Windows.Forms.Label();
            this.ResetWool = new System.Windows.Forms.Label();
            this.HaveWheat = new System.Windows.Forms.Label();
            this.ResetWheat = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Stone
            // 
            this.Stone.Location = new System.Drawing.Point(9, 52);
            this.Stone.Name = "Stone";
            this.Stone.Size = new System.Drawing.Size(75, 23);
            this.Stone.TabIndex = 0;
            this.Stone.Text = "Руда";
            this.Stone.UseVisualStyleBackColor = true;
            this.Stone.Click += new System.EventHandler(this.Stone_Click);
            // 
            // Glay
            // 
            this.Glay.Location = new System.Drawing.Point(9, 78);
            this.Glay.Name = "Glay";
            this.Glay.Size = new System.Drawing.Size(75, 23);
            this.Glay.TabIndex = 1;
            this.Glay.Text = "Глина";
            this.Glay.UseVisualStyleBackColor = true;
            this.Glay.Click += new System.EventHandler(this.Glay_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Игроку 1 нужно сбросить:";
            // 
            // Wood
            // 
            this.Wood.Location = new System.Drawing.Point(9, 106);
            this.Wood.Name = "Wood";
            this.Wood.Size = new System.Drawing.Size(75, 23);
            this.Wood.TabIndex = 3;
            this.Wood.Text = "Дерево";
            this.Wood.UseVisualStyleBackColor = true;
            this.Wood.Click += new System.EventHandler(this.Wood_Click);
            // 
            // Wool
            // 
            this.Wool.Location = new System.Drawing.Point(9, 135);
            this.Wool.Name = "Wool";
            this.Wool.Size = new System.Drawing.Size(75, 23);
            this.Wool.TabIndex = 4;
            this.Wool.Text = "Шерсть";
            this.Wool.UseVisualStyleBackColor = true;
            this.Wool.Click += new System.EventHandler(this.Wool_Click);
            // 
            // Whaet
            // 
            this.Whaet.Location = new System.Drawing.Point(9, 164);
            this.Whaet.Name = "Whaet";
            this.Whaet.Size = new System.Drawing.Size(75, 23);
            this.Whaet.TabIndex = 5;
            this.Whaet.Text = "Пшеница";
            this.Whaet.UseVisualStyleBackColor = true;
            this.Whaet.Click += new System.EventHandler(this.Whaet_Click);
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(210, 52);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(116, 31);
            this.Back.TabIndex = 6;
            this.Back.Text = " Заново";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // Acept
            // 
            this.Acept.Location = new System.Drawing.Point(210, 151);
            this.Acept.Name = "Acept";
            this.Acept.Size = new System.Drawing.Size(116, 36);
            this.Acept.TabIndex = 7;
            this.Acept.Text = "Подтвердить сброс";
            this.Acept.UseVisualStyleBackColor = true;
            this.Acept.Click += new System.EventHandler(this.Acept_Click);
            // 
            // HaveStone
            // 
            this.HaveStone.AutoSize = true;
            this.HaveStone.Location = new System.Drawing.Point(107, 57);
            this.HaveStone.Name = "HaveStone";
            this.HaveStone.Size = new System.Drawing.Size(13, 13);
            this.HaveStone.TabIndex = 8;
            this.HaveStone.Text = "0";
            // 
            // ResetStone
            // 
            this.ResetStone.AutoSize = true;
            this.ResetStone.Location = new System.Drawing.Point(161, 57);
            this.ResetStone.Name = "ResetStone";
            this.ResetStone.Size = new System.Drawing.Size(13, 13);
            this.ResetStone.TabIndex = 9;
            this.ResetStone.Text = "0";
            // 
            // HaveGlay
            // 
            this.HaveGlay.AutoSize = true;
            this.HaveGlay.Location = new System.Drawing.Point(107, 83);
            this.HaveGlay.Name = "HaveGlay";
            this.HaveGlay.Size = new System.Drawing.Size(13, 13);
            this.HaveGlay.TabIndex = 10;
            this.HaveGlay.Text = "0";
            // 
            // ResetGlay
            // 
            this.ResetGlay.AutoSize = true;
            this.ResetGlay.Location = new System.Drawing.Point(161, 83);
            this.ResetGlay.Name = "ResetGlay";
            this.ResetGlay.Size = new System.Drawing.Size(13, 13);
            this.ResetGlay.TabIndex = 11;
            this.ResetGlay.Text = "0";
            // 
            // HaveWood
            // 
            this.HaveWood.AutoSize = true;
            this.HaveWood.Location = new System.Drawing.Point(107, 111);
            this.HaveWood.Name = "HaveWood";
            this.HaveWood.Size = new System.Drawing.Size(13, 13);
            this.HaveWood.TabIndex = 12;
            this.HaveWood.Text = "0";
            // 
            // ResetWood
            // 
            this.ResetWood.AutoSize = true;
            this.ResetWood.Location = new System.Drawing.Point(161, 111);
            this.ResetWood.Name = "ResetWood";
            this.ResetWood.Size = new System.Drawing.Size(13, 13);
            this.ResetWood.TabIndex = 13;
            this.ResetWood.Text = "0";
            // 
            // HaveWool
            // 
            this.HaveWool.AutoSize = true;
            this.HaveWool.Location = new System.Drawing.Point(107, 140);
            this.HaveWool.Name = "HaveWool";
            this.HaveWool.Size = new System.Drawing.Size(13, 13);
            this.HaveWool.TabIndex = 14;
            this.HaveWool.Text = "0";
            // 
            // ResetWool
            // 
            this.ResetWool.AutoSize = true;
            this.ResetWool.Location = new System.Drawing.Point(161, 140);
            this.ResetWool.Name = "ResetWool";
            this.ResetWool.Size = new System.Drawing.Size(13, 13);
            this.ResetWool.TabIndex = 15;
            this.ResetWool.Text = "0";
            // 
            // HaveWheat
            // 
            this.HaveWheat.AutoSize = true;
            this.HaveWheat.Location = new System.Drawing.Point(107, 169);
            this.HaveWheat.Name = "HaveWheat";
            this.HaveWheat.Size = new System.Drawing.Size(13, 13);
            this.HaveWheat.TabIndex = 16;
            this.HaveWheat.Text = "0";
            // 
            // ResetWheat
            // 
            this.ResetWheat.AutoSize = true;
            this.ResetWheat.Location = new System.Drawing.Point(161, 169);
            this.ResetWheat.Name = "ResetWheat";
            this.ResetWheat.Size = new System.Drawing.Size(13, 13);
            this.ResetWheat.TabIndex = 17;
            this.ResetWheat.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(84, 37);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "В наличии";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(148, 37);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Выбрано";
            // 
            // ResetCart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.ResetWheat);
            this.Controls.Add(this.HaveWheat);
            this.Controls.Add(this.ResetWool);
            this.Controls.Add(this.HaveWool);
            this.Controls.Add(this.ResetWood);
            this.Controls.Add(this.HaveWood);
            this.Controls.Add(this.ResetGlay);
            this.Controls.Add(this.HaveGlay);
            this.Controls.Add(this.ResetStone);
            this.Controls.Add(this.HaveStone);
            this.Controls.Add(this.Acept);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.Whaet);
            this.Controls.Add(this.Wool);
            this.Controls.Add(this.Wood);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Glay);
            this.Controls.Add(this.Stone);
            this.Name = "ResetCart";
            this.Size = new System.Drawing.Size(343, 198);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Stone;
        private System.Windows.Forms.Button Glay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Wood;
        private System.Windows.Forms.Button Wool;
        private System.Windows.Forms.Button Whaet;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button Acept;
        private System.Windows.Forms.Label HaveStone;
        private System.Windows.Forms.Label ResetStone;
        private System.Windows.Forms.Label HaveGlay;
        private System.Windows.Forms.Label ResetGlay;
        private System.Windows.Forms.Label HaveWood;
        private System.Windows.Forms.Label ResetWood;
        private System.Windows.Forms.Label HaveWool;
        private System.Windows.Forms.Label ResetWool;
        private System.Windows.Forms.Label HaveWheat;
        private System.Windows.Forms.Label ResetWheat;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}
