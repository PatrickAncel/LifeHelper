namespace LifeHelper
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnShowActiveTasks = new System.Windows.Forms.Button();
            this.btnOpenCreateTaskForm = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.lblRowCount = new System.Windows.Forms.Label();
            this.btnShowLow = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnShowMed = new System.Windows.Forms.Button();
            this.btnShowHigh = new System.Windows.Forms.Button();
            this.btnShowInactive = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTaskIDInput = new System.Windows.Forms.TextBox();
            this.btnOpenUpdateTaskForm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 236);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1892, 551);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView1_RowPrePaint);
            // 
            // btnShowActiveTasks
            // 
            this.btnShowActiveTasks.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowActiveTasks.Location = new System.Drawing.Point(16, 26);
            this.btnShowActiveTasks.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnShowActiveTasks.Name = "btnShowActiveTasks";
            this.btnShowActiveTasks.Size = new System.Drawing.Size(143, 68);
            this.btnShowActiveTasks.TabIndex = 7;
            this.btnShowActiveTasks.Text = "Show Active Tasks";
            this.btnShowActiveTasks.UseVisualStyleBackColor = true;
            this.btnShowActiveTasks.Click += new System.EventHandler(this.btnShowActiveTasks_Click);
            // 
            // btnOpenCreateTaskForm
            // 
            this.btnOpenCreateTaskForm.Location = new System.Drawing.Point(1124, 803);
            this.btnOpenCreateTaskForm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpenCreateTaskForm.Name = "btnOpenCreateTaskForm";
            this.btnOpenCreateTaskForm.Size = new System.Drawing.Size(156, 62);
            this.btnOpenCreateTaskForm.TabIndex = 8;
            this.btnOpenCreateTaskForm.Text = "Add New Task";
            this.btnOpenCreateTaskForm.UseVisualStyleBackColor = true;
            this.btnOpenCreateTaskForm.Click += new System.EventHandler(this.btnOpenCreateTaskForm_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(165, 26);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(143, 68);
            this.button5.TabIndex = 9;
            this.button5.Text = "Show Task Counts";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // lblRowCount
            // 
            this.lblRowCount.AutoSize = true;
            this.lblRowCount.Location = new System.Drawing.Point(12, 214);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(92, 20);
            this.lblRowCount.TabIndex = 10;
            this.lblRowCount.Text = "Row Count:";
            // 
            // btnShowLow
            // 
            this.btnShowLow.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowLow.Location = new System.Drawing.Point(364, 38);
            this.btnShowLow.Name = "btnShowLow";
            this.btnShowLow.Size = new System.Drawing.Size(209, 45);
            this.btnShowLow.TabIndex = 11;
            this.btnShowLow.Text = "Low";
            this.btnShowLow.UseVisualStyleBackColor = true;
            this.btnShowLow.Click += new System.EventHandler(this.btnShowLow_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(360, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Show Active Tasks by Priority";
            // 
            // btnShowMed
            // 
            this.btnShowMed.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowMed.Location = new System.Drawing.Point(364, 89);
            this.btnShowMed.Name = "btnShowMed";
            this.btnShowMed.Size = new System.Drawing.Size(209, 45);
            this.btnShowMed.TabIndex = 13;
            this.btnShowMed.Text = "Medium";
            this.btnShowMed.UseVisualStyleBackColor = true;
            this.btnShowMed.Click += new System.EventHandler(this.btnShowMed_Click);
            // 
            // btnShowHigh
            // 
            this.btnShowHigh.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowHigh.Location = new System.Drawing.Point(364, 140);
            this.btnShowHigh.Name = "btnShowHigh";
            this.btnShowHigh.Size = new System.Drawing.Size(209, 45);
            this.btnShowHigh.TabIndex = 14;
            this.btnShowHigh.Text = "High";
            this.btnShowHigh.UseVisualStyleBackColor = true;
            this.btnShowHigh.Click += new System.EventHandler(this.btnShowHigh_Click);
            // 
            // btnShowInactive
            // 
            this.btnShowInactive.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowInactive.Location = new System.Drawing.Point(579, 38);
            this.btnShowInactive.Name = "btnShowInactive";
            this.btnShowInactive.Size = new System.Drawing.Size(209, 45);
            this.btnShowInactive.TabIndex = 15;
            this.btnShowInactive.Text = "Show Inactive Tasks";
            this.btnShowInactive.UseVisualStyleBackColor = true;
            this.btnShowInactive.Click += new System.EventHandler(this.btnShowInactive_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(564, 824);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Task ID";
            // 
            // txtTaskIDInput
            // 
            this.txtTaskIDInput.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaskIDInput.Location = new System.Drawing.Point(634, 821);
            this.txtTaskIDInput.Name = "txtTaskIDInput";
            this.txtTaskIDInput.Size = new System.Drawing.Size(111, 26);
            this.txtTaskIDInput.TabIndex = 17;
            // 
            // btnOpenUpdateTaskForm
            // 
            this.btnOpenUpdateTaskForm.Location = new System.Drawing.Point(751, 803);
            this.btnOpenUpdateTaskForm.Name = "btnOpenUpdateTaskForm";
            this.btnOpenUpdateTaskForm.Size = new System.Drawing.Size(166, 62);
            this.btnOpenUpdateTaskForm.TabIndex = 18;
            this.btnOpenUpdateTaskForm.Text = "Update Task";
            this.btnOpenUpdateTaskForm.UseVisualStyleBackColor = true;
            this.btnOpenUpdateTaskForm.Click += new System.EventHandler(this.btnOpenUpdateTaskForm_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1913, 889);
            this.Controls.Add(this.btnOpenUpdateTaskForm);
            this.Controls.Add(this.txtTaskIDInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnShowInactive);
            this.Controls.Add(this.btnShowHigh);
            this.Controls.Add(this.btnShowMed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnShowLow);
            this.Controls.Add(this.lblRowCount);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.btnOpenCreateTaskForm);
            this.Controls.Add(this.btnShowActiveTasks);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Life Helper";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnShowActiveTasks;
        private System.Windows.Forms.Button btnOpenCreateTaskForm;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label lblRowCount;
        private System.Windows.Forms.Button btnShowLow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnShowMed;
        private System.Windows.Forms.Button btnShowHigh;
        private System.Windows.Forms.Button btnShowInactive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTaskIDInput;
        private System.Windows.Forms.Button btnOpenUpdateTaskForm;
    }
}

