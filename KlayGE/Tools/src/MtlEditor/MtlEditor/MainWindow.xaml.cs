﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Controls.Ribbon;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Windows.Forms;

namespace MtlEditor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : RibbonWindow
	{
		enum PropertyOrders
		{
			PO_Albedo = 0,
			PO_Metalness,
			PO_Glossiness,
			PO_Emissive,
			PO_EmissiveMultiplier,
			PO_Opacity,
			PO_AlbedoTex,
			PO_MetalnessTex,
			PO_GlossinessTex,
			PO_EmissiveTex,
			PO_NormalTex,
			PO_HeightTex,
			PO_DetailMode,
			PO_HeightOffset,
			PO_HeightScale,
			PO_EdgeTessHint,
			PO_InsideTessHint,
			PO_MinTess,
			PO_MaxTess,
			PO_Transparent,
			PO_AlphaTest,
			PO_SSS
		};

		[CategoryOrder("Material", 0)]
		[CategoryOrder("Textures", 1)]
		[CategoryOrder("Detail", 2)]
		[CategoryOrder("Attributes", 3)]
		public class ModelPropertyTypes
		{
			[Category("Material")]
			[DisplayName("Albedo")]
			[PropertyOrder((int)PropertyOrders.PO_Albedo)]
			public Color albedo { get; set; }
			[Category("Material")]
			[DisplayName("Metalness")]
			[Editor(typeof(SliderUserControlEditor), typeof(SliderUserControlEditor))]
			[PropertyOrder((int)PropertyOrders.PO_Metalness)]
			public float metalness { get; set; }
			[Category("Material")]
			[DisplayName("Glossiness")]
			[Editor(typeof(SliderUserControlEditor), typeof(SliderUserControlEditor))]
			[PropertyOrder((int)PropertyOrders.PO_Glossiness)]
			public float glossiness { get; set; }
			[Category("Material")]
			[DisplayName("Emissive")]
			[PropertyOrder((int)PropertyOrders.PO_Emissive)]
			public Color emissive { get; set; }
			[Category("Material")]
			[DisplayName("Emissive Multiplier")]
			[Editor(typeof(MultiplierSliderUserControlEditor), typeof(SliderUserControlEditor))]
			[PropertyOrder((int)PropertyOrders.PO_EmissiveMultiplier)]
			public float emissive_multiplier { get; set; }
			[Category("Material")]
			[DisplayName("Opacity")]
			[PropertyOrder((int)PropertyOrders.PO_Opacity)]
			[Editor(typeof(SliderUserControlEditor), typeof(SliderUserControlEditor))]
			public float opacity { get; set; }

			[Category("Textures")]
			[DisplayName("Albedo")]
			[PropertyOrder((int)PropertyOrders.PO_AlbedoTex)]
			[Editor(typeof(OpenTexUserControlEditor), typeof(OpenTexUserControlEditor))]
			public string albedo_tex { get; set; }
			[Category("Textures")]
			[DisplayName("Metalness")]
			[PropertyOrder((int)PropertyOrders.PO_MetalnessTex)]
			[Editor(typeof(OpenTexUserControlEditor), typeof(OpenTexUserControlEditor))]
			public string metalness_tex { get; set; }
			[Category("Textures")]
			[DisplayName("Glossiness")]
			[PropertyOrder((int)PropertyOrders.PO_GlossinessTex)]
			[Editor(typeof(OpenTexUserControlEditor), typeof(OpenTexUserControlEditor))]
			public string glossiness_tex { get; set; }
			[Category("Textures")]
			[DisplayName("Emissive")]
			[PropertyOrder((int)PropertyOrders.PO_EmissiveTex)]
			[Editor(typeof(OpenTexUserControlEditor), typeof(OpenTexUserControlEditor))]
			public string emissive_tex { get; set; }
			[Category("Textures")]
			[DisplayName("Normal")]
			[PropertyOrder((int)PropertyOrders.PO_NormalTex)]
			[Editor(typeof(OpenTexUserControlEditor), typeof(OpenTexUserControlEditor))]
			public string normal_tex { get; set; }
			[Category("Textures")]
			[DisplayName("Height")]
			[PropertyOrder((int)PropertyOrders.PO_HeightTex)]
			[Editor(typeof(OpenTexUserControlEditor), typeof(OpenTexUserControlEditor))]
			public string height_tex { get; set; }

			[Category("Detail")]
			[DisplayName("Mode")]
			[ItemsSource(typeof(DetailModeItemsSource))]
			[PropertyOrder((int)PropertyOrders.PO_DetailMode)]
			public string detail_mode { get; set; }
			[Category("Detail")]
			[DisplayName("Height Offset")]
			[PropertyOrder((int)PropertyOrders.PO_HeightOffset)]
			public float height_offset { get; set; }
			[Category("Detail")]
			[DisplayName("Height Scale")]
			[PropertyOrder((int)PropertyOrders.PO_HeightScale)]
			public float height_scale { get; set; }
			[Category("Detail")]
			[DisplayName("Edge Tessellation Hint")]
			[PropertyOrder((int)PropertyOrders.PO_EdgeTessHint)]
			public float edge_tess_hint { get; set; }
			[Category("Detail")]
			[DisplayName("Inside Tessellation Hint")]
			[PropertyOrder((int)PropertyOrders.PO_InsideTessHint)]
			public float inside_tess_hint { get; set; }
			[Category("Detail")]
			[DisplayName("Min Tessellation")]
			[PropertyOrder((int)PropertyOrders.PO_MinTess)]
			public float min_tess { get; set; }
			[Category("Detail")]
			[DisplayName("Max Tessellation")]
			[PropertyOrder((int)PropertyOrders.PO_MaxTess)]
			public float max_tess { get; set; }

			[Category("Attributes")]
			[DisplayName("Transparent")]
			[PropertyOrder((int)PropertyOrders.PO_Transparent)]
			public bool transparent { get; set; }
			[Category("Attributes")]
			[DisplayName("Alpha Test")]
			[PropertyOrder((int)PropertyOrders.PO_AlphaTest)]
			[Editor(typeof(SliderUserControlEditor), typeof(SliderUserControlEditor))]
			public float alpha_test { get; set; }
			[Category("Attributes")]
			[DisplayName("SSS")]
			[PropertyOrder((int)PropertyOrders.PO_SSS)]
			public bool sss { get; set; }
		}

		public MainWindow()
		{
			InitializeComponent();

			DataContext = this;

			var model_root = new MeshEntityViewModel[1];
			var model = new MeshEntity();
			model.ID = 0;
			model.Name = "Model";
			model_root[0] = new MeshEntityViewModel(this, model);
			meshes_ = new ReadOnlyCollection<MeshEntityViewModel>(model_root);

			var material_root = new MaterialEntityViewModel[1];
			var material = new MaterialEntity();
			material.ID = 0;
			material.Name = "Material";
			material_root[0] = new MaterialEntityViewModel(this, material);
			materials_ = new ReadOnlyCollection<MaterialEntityViewModel>(material_root);

			DetailModeItemsSource.items = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
			DetailModeItemsSource.items.Clear();
			DetailModeItemsSource.items.Add("Parallax");
			DetailModeItemsSource.items.Add("Flat Tessellation");
			DetailModeItemsSource.items.Add("Smooth Tessellation");

			properties_obj_ = new ModelPropertyTypes();
			properties.SelectedObject = properties_obj_;

			save.IsEnabled = false;
			save_as.IsEnabled = false;
			undo.IsEnabled = false;
			redo.IsEnabled = false;
			skinning.IsEnabled = false;
			play.IsEnabled = false;
			visualize.IsEnabled = false;
			frame_text.IsEnabled = false;
			frame_slider.IsEnabled = false;

			frame_slider.Minimum = 0;
			frame_slider.Maximum = 1;

			properties.IsEnabled = false;

			last_time_ = DateTime.Now;

			Uri iconUri = new Uri("pack://application:,,,/Images/klayge_logo.ico", UriKind.RelativeOrAbsolute);
			this.Icon = BitmapFrame.Create(iconUri);
		}

		void MainWindowLoaded(object sender, RoutedEventArgs e)
		{
			IntPtr wnd = editor_wnd.Handle;
			core_ = new KlayGE.MtlEditorCoreWrapper(wnd);

			core_.UpdateSelectEntityCallback(this.UpdateSelectMeshEntity);

			CompositionTarget.Rendering += this.MainWindowIdle;
		}

		void MainWindowUnloaded(object sender, RoutedEventArgs e)
		{
			CompositionTarget.Rendering -= this.MainWindowIdle;
			core_.Dispose();
			core_ = null;
		}

		private void MainWindowIdle(object sender, EventArgs e)
		{
			core_.Refresh();

			if (play_)
			{
				DateTime this_time = DateTime.Now;
				if (this_time.Subtract(last_time_).TotalSeconds > 0.02)
				{
					frame_ += 0.02 * core_.ModelFrameRate();
					frame_ = frame_ % (float)core_.NumFrames();

					last_time_ = this_time;
				}

				frame_slider.Value = frame_;
			}
		}

		private void EditorWindowSizeChanged(object sender, SizeChangedEventArgs e)
		{
			editor_frame.Width = editor_bg.ActualWidth;
			editor_frame.Height = editor_bg.ActualHeight;

			core_.Resize((uint)editor_frame.Width, (uint)editor_frame.Height);
		}

		private void OpenModel(string file_name)
		{
			if (!core_.OpenModel(file_name.ToLower()))
			{
				return;
			}

			this.FileNameChanged(file_name);

			this.ClearHistroy();
			this.UpdateHistroy();

			save.IsEnabled = true;
			save_as.IsEnabled = true;
			if (core_.NumFrames() != 0)
			{
				skinning.IsEnabled = true;
				skinning.IsChecked = true;
				play.IsEnabled = true;
				frame_text.IsEnabled = true;
				frame_slider.IsEnabled = true;
				frame_slider.Maximum = core_.NumFrames() - 1;
			}
			else
			{
				skinning.IsEnabled = false;
				skinning.IsChecked = false;
				play.IsEnabled = false;
				frame_text.IsEnabled = false;
				frame_slider.IsEnabled = false;
				frame_slider.Maximum = 1;
			}
			visualize.IsEnabled = true;
			properties.IsEnabled = true;
			// Workround for.NET 4.6.1
			visualize_gallery.Command = ApplicationCommands.Print;
			visualize_gallery.Command = null;

			frame_ = 0;

			meshes_[0].Children.Clear();
			materials_[0].Children.Clear();
			uint num_meshes = core_.NumMeshes();
			for (uint i = 0; i < num_meshes; ++ i)
			{
				var entity = new MeshEntity();
				entity.ID = i + 1;
				entity.Name = core_.MeshName(i);
				meshes_[0].Children.Add(new MeshEntityViewModel(this, entity));
			}
			this.SelectMeshEntity(0);

			uint num_mtls = core_.NumMaterials();
			for (uint i = 0; i < num_mtls; ++ i)
			{
				var entity = new MaterialEntity();
				entity.ID = i + 1;
				entity.Name = "Material " + i;
				materials_[0].Children.Add(new MaterialEntityViewModel(this, entity));
			}
			this.SelectMaterialEntity(0);

			this.UpdateMeshProperties(0);

			properties.SelectedObject = null;
			properties.SelectedObject = properties_obj_;
		}

		private void OpenClick(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

			dlg.DefaultExt = ".meshml";
			dlg.Filter = "All Model Files|*.meshml;*.model_bin;*.3ds;*.ac;*.ase;*.assbin;*.assxml;*.b3d;*.bvh;*.collada;*.dxf;*.csm;"
				+ "*.hmp;*.irr;*.lwo;*.lws;*.md2;*.md3;*.md5;*.mdc;*.mdl;*.nff;*.ndo;*.off;*.obj;*.ogre;*.opengex;*.ply;*.ms3d;*.cob;"
				+ "*.blend;*.ifc;*.xgl;*.fbx;*.q3d;*.q3bsp;*.raw;*.smd;*.stl;*.terragen;*.3d;*.x|All Files|*.*";
			dlg.CheckPathExists = true;
			dlg.CheckFileExists = true;
			if (true == dlg.ShowDialog())
			{
				this.OpenModel(dlg.FileName);
			}
		}

		private void SaveClick(object sender, RoutedEventArgs e)
		{
			core_.SaveModel(opened_file_);
		}

		private void SaveAsClick(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

			dlg.DefaultExt = ".meshml";
			dlg.Filter = "MeshML Files (*.meshml)|*.meshml|All Files|*.*";
			dlg.OverwritePrompt = true;
			if (true == dlg.ShowDialog())
			{
				core_.SaveModel(dlg.FileName);
				this.FileNameChanged(dlg.FileName);
			}
		}

		private void UndoClick(object sender, RoutedEventArgs e)
		{
			this.Undo();

			selected_mesh_id_ = core_.SelectedMesh();
			this.UpdateMeshProperties(selected_mesh_id_);
			this.UpdateHistroy();
		}

		private void RedoClick(object sender, RoutedEventArgs e)
		{
			this.Redo();

			selected_mesh_id_ = core_.SelectedMesh();
			this.UpdateMeshProperties(selected_mesh_id_);
			this.UpdateHistroy();
		}

		public bool SkinningValue
		{
			get
			{
				return skinning_;
			}
			set
			{
				skinning_ = value;
				core_.SkinningOn(skinning_ ? 1 : 0);
				play.IsEnabled = skinning_;
			}
		}

		public bool PlayValue
		{
			get
			{
				return play_;
			}
			set
			{
				play_ = value;
			}
		}

		public bool FPSCameraValue
		{
			get
			{
				return fps_camera_;
			}
			set
			{
				fps_camera_ = value;
				core_.FPSCameraOn(fps_camera_ ? 1 : 0);
			}
		}

		public bool LineModeValue
		{
			get
			{
				return line_mode_;
			}
			set
			{
				line_mode_ = value;
				core_.LineModeOn(line_mode_ ? 1 : 0);
			}
		}

		public bool ImposterModeValue
		{
			get
			{
				return imposter_mode_;
			}
			set
			{
				imposter_mode_ = value;
				core_.ImposterModeOn(imposter_mode_ ? 1 : 0);
			}
		}

		public double FrameSliderValue
		{
			get
			{
				return frame_;
			}
			set
			{
				frame_ = value;
				core_.CurrFrame((float)frame_);
				frame_text.Content = "Frame " + (int)(frame_ + 0.5f);

				this.UpdateHistroy();
			}
		}

		private void VisualizeSelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			if (core_ != null)
			{
				System.Windows.Controls.Ribbon.RibbonGalleryItem item = e.NewValue as System.Windows.Controls.Ribbon.RibbonGalleryItem;
				core_.Visualize(Int32.Parse((string)item.DataContext));
			}
		}

		private void EditorMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			uint buttons = 0;
			if (MouseButtons.Left == e.Button)
			{
				buttons |= 1;
			}
			if (MouseButtons.Right == e.Button)
			{
				buttons |= 2;
			}
			if (MouseButtons.Middle == e.Button)
			{
				buttons |= 4;
			}
			core_.MouseDown(e.X, e.Y, buttons);
		}

		private void EditorMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			uint buttons = 0;
			if (MouseButtons.Left == e.Button)
			{
				buttons |= 1;
			}
			if (MouseButtons.Right == e.Button)
			{
				buttons |= 2;
			}
			if (MouseButtons.Middle == e.Button)
			{
				buttons |= 4;
			}
			core_.MouseUp(e.X, e.Y, buttons);

			if (MouseButtons.Left == e.Button)
			{
				uint selected_mesh = core_.SelectedMesh();
				if (selected_mesh != selected_mesh_id_)
				{
					this.UpdateMeshProperties(selected_mesh);
					this.UpdateHistroy();
				}
			}
		}

		private void EditorMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			uint buttons = 0;
			if (MouseButtons.Left == e.Button)
			{
				buttons |= 1;
			}
			if (MouseButtons.Right == e.Button)
			{
				buttons |= 2;
			}
			if (MouseButtons.Middle == e.Button)
			{
				buttons |= 4;
			}
			core_.MouseMove(e.X, e.Y, buttons);
		}

		private void EditorKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			core_.KeyPress(e.KeyChar);
		}

		private void UpdateSelectMeshEntity(uint mesh_id)
		{
			foreach (var item in meshes_)
			{
				foreach (var entity in item.Children)
				{
					entity.IsSelected = false;
				}
			}

			if (mesh_id > 0)
			{
				foreach (var item in meshes_)
				{
					foreach (var entity in item.Children)
					{
						if (entity.Entity.ID == mesh_id)
						{
							entity.IsSelected = true;
						}
					}
				}
			}
			else
			{
				this.SelectMeshEntity(mesh_id);
			}
		}

		private void UpdateMeshProperties(uint mesh_id)
		{
			selected_mesh_id_ = mesh_id;
			if (mesh_id > 0)
			{
				uint mtl_id = core_.MaterialID(mesh_id) + 1;

				foreach (var item in materials_)
				{
					foreach (var entity in item.Children)
					{
						entity.IsSelected = false;
					}
				}

				if (mtl_id > 0)
				{
					foreach (var item in materials_)
					{
						foreach (var entity in item.Children)
						{
							if (entity.Entity.ID == mtl_id)
							{
								entity.IsSelected = true;
							}
						}
					}
				}
				else
				{
					this.SelectMaterialEntity(mtl_id);
				}
			}
		}

		private void UpdateMaterialProperties(uint mtl_id)
		{
			selected_mtl_id_ = mtl_id;

			properties.SelectedObject = null;

			if (mtl_id > 0)
			{
				-- mtl_id;

				properties_obj_.albedo = this.FloatPtrToLDRColor(core_.AlbedoMaterial(mtl_id), 1);
				properties_obj_.metalness = core_.MetalnessMaterial(mtl_id);
				properties_obj_.glossiness = core_.GlossinessMaterial(mtl_id);
				properties_obj_.emissive_multiplier = this.FloatPtrToMultipiler(core_.EmissiveMaterial(mtl_id));
				properties_obj_.emissive = this.FloatPtrToLDRColor(core_.EmissiveMaterial(mtl_id), properties_obj_.emissive_multiplier);
				properties_obj_.opacity = core_.OpacityMaterial(mtl_id);

				properties_obj_.albedo_tex = core_.AlbedoTexture(mtl_id);
				properties_obj_.metalness_tex = core_.MetalnessTexture(mtl_id);
				properties_obj_.glossiness_tex = core_.GlossinessTexture(mtl_id);
				properties_obj_.emissive_tex = core_.EmissiveTexture(mtl_id);
				properties_obj_.normal_tex = core_.NormalTexture(mtl_id);
				properties_obj_.height_tex = core_.HeightTexture(mtl_id);

				properties_obj_.detail_mode = DetailModeItemsSource.items[(int)core_.DetailMode(mtl_id)].DisplayName;
				properties_obj_.height_offset = core_.HeightOffset(mtl_id);
				properties_obj_.height_scale = core_.HeightScale(mtl_id);
				properties_obj_.edge_tess_hint = core_.EdgeTessHint(mtl_id);
				properties_obj_.inside_tess_hint = core_.InsideTessHint(mtl_id);
				properties_obj_.min_tess = core_.MinTess(mtl_id);
				properties_obj_.max_tess = core_.MaxTess(mtl_id);

				properties_obj_.transparent = core_.TransparentMaterial(mtl_id);
				properties_obj_.alpha_test = core_.AlphaTestMaterial(mtl_id);
				properties_obj_.sss = core_.SSSMaterial(mtl_id);
			}
			else
			{
				properties_obj_.albedo = Color.FromArgb(0, 0, 0, 0);
				properties_obj_.metalness = 0;
				properties_obj_.glossiness = 0;
				properties_obj_.emissive = Color.FromArgb(0, 0, 0, 0);
				properties_obj_.opacity = 1;

				properties_obj_.albedo_tex = "";
				properties_obj_.metalness_tex = "";
				properties_obj_.glossiness_tex = "";
				properties_obj_.emissive_tex = "";
				properties_obj_.normal_tex = "";
				properties_obj_.height_tex = "";

				properties_obj_.detail_mode = "Parallax";
				properties_obj_.height_offset = -0.5f;
				properties_obj_.height_scale = 0.06f;
				properties_obj_.edge_tess_hint = 5;
				properties_obj_.inside_tess_hint = 5;
				properties_obj_.min_tess = 1;
				properties_obj_.max_tess = 9;

				properties_obj_.transparent = false;
				properties_obj_.alpha_test = 0;
				properties_obj_.sss = false;
			}

			properties.SelectedObject = properties_obj_;
		}

		private void PropertyGridValueChanged(object sender, Xceed.Wpf.Toolkit.PropertyGrid.PropertyValueChangedEventArgs e)
		{
			Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem item = e.OriginalSource as Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem;
			switch ((PropertyOrders)item.PropertyOrder)
			{
				case PropertyOrders.PO_Albedo:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						float[] albedo = ColorToFloatPtr(properties_obj_.albedo, 1);
						float[] old_albedo = core_.AlbedoMaterial(mtl_id);
						if (!this.FloatEqual(old_albedo[0], albedo[0]) || !this.FloatEqual(old_albedo[1], albedo[1])
							|| !this.FloatEqual(old_albedo[2], albedo[2]))
						{
							this.ExecuteCommand(new MtlEditorCommandSetAlbedoMaterial(core_, mtl_id, albedo));
						}
					}
					break;

				case PropertyOrders.PO_Metalness:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						if (!this.FloatEqual(core_.MetalnessMaterial(mtl_id), properties_obj_.metalness))
						{
							this.ExecuteCommand(new MtlEditorCommandSetMetalnessMaterial(core_, mtl_id, properties_obj_.metalness));
						}
					}
					break;

				case PropertyOrders.PO_Glossiness:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						if (!this.FloatEqual(core_.GlossinessMaterial(mtl_id), properties_obj_.glossiness))
						{
							this.ExecuteCommand(new MtlEditorCommandSetGlossinessMaterial(core_, mtl_id, properties_obj_.glossiness));
						}
					}
					break;

				case PropertyOrders.PO_Emissive:
				case PropertyOrders.PO_EmissiveMultiplier:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						float[] emissive = ColorToFloatPtr(properties_obj_.emissive, properties_obj_.emissive_multiplier);
						float[] old_emissive = core_.EmissiveMaterial(mtl_id);
						if (!this.FloatEqual(old_emissive[0], emissive[0]) || !this.FloatEqual(old_emissive[1], emissive[1])
							|| !this.FloatEqual(old_emissive[2], emissive[2]))
						{
							this.ExecuteCommand(new MtlEditorCommandSetEmissiveMaterial(core_, mtl_id, emissive));
						}
					}
					break;

				case PropertyOrders.PO_Opacity:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						if (!this.FloatEqual(core_.OpacityMaterial(mtl_id), properties_obj_.opacity))
						{
							this.ExecuteCommand(new MtlEditorCommandSetOpacityMaterial(core_, mtl_id, properties_obj_.opacity));
						}
					}
					break;

				case PropertyOrders.PO_AlbedoTex:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						string albedo_tex = RelativePath(properties_obj_.albedo_tex);
						if (core_.AlbedoTexture(mtl_id) != albedo_tex)
						{
							this.ExecuteCommand(new MtlEditorCommandSetAlbedoTexture(core_, mtl_id, albedo_tex));
						}
					}
					break;

				case PropertyOrders.PO_MetalnessTex:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						string metalness_tex = RelativePath(properties_obj_.metalness_tex);
						if (core_.MetalnessTexture(mtl_id) != metalness_tex)
						{
							this.ExecuteCommand(new MtlEditorCommandSetMetalnessTexture(core_, mtl_id, metalness_tex));
						}
					}
					break;

				case PropertyOrders.PO_GlossinessTex:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						string glossiness_tex = RelativePath(properties_obj_.glossiness_tex);
						if (core_.GlossinessTexture(mtl_id) != glossiness_tex)
						{
							this.ExecuteCommand(new MtlEditorCommandSetGlossinessTexture(core_, mtl_id, glossiness_tex));
						}
					}
					break;

				case PropertyOrders.PO_EmissiveTex:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						string emissive_tex = RelativePath(properties_obj_.emissive_tex);
						if (core_.EmissiveTexture(mtl_id) != emissive_tex)
						{
							this.ExecuteCommand(new MtlEditorCommandSetEmissiveTexture(core_, mtl_id, emissive_tex));
						}
					}
					break;

				case PropertyOrders.PO_NormalTex:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						string normal_tex = RelativePath(properties_obj_.normal_tex);
						if (core_.NormalTexture(mtl_id) != normal_tex)
						{
							this.ExecuteCommand(new MtlEditorCommandSetNormalTexture(core_, mtl_id, normal_tex));
						}
					}
					break;

				case PropertyOrders.PO_HeightTex:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						string height_tex = RelativePath(properties_obj_.height_tex);
						if (core_.HeightTexture(mtl_id) != height_tex)
						{
							this.ExecuteCommand(new MtlEditorCommandSetHeightTexture(core_, mtl_id, height_tex));
						}
					}
					break;

				case PropertyOrders.PO_DetailMode:
					if (selected_mtl_id_ > 0)
					{
						uint mode = 0;
						for (; mode < DetailModeItemsSource.items.Count; ++ mode)
						{
							if (DetailModeItemsSource.items[(int)mode].DisplayName == (e.NewValue as string))
							{
								break;
							}
						}

						uint mtl_id = selected_mtl_id_ - 1;
						if (core_.DetailMode(mtl_id) != mode)
						{
							this.ExecuteCommand(new MtlEditorCommandSetDetailMode(core_, mtl_id, mode));
						}
					}
					break;

				case PropertyOrders.PO_HeightOffset:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						if (!this.FloatEqual(core_.HeightOffset(mtl_id), properties_obj_.height_offset))
						{
							this.ExecuteCommand(new MtlEditorCommandSetHeightOffset(core_, mtl_id, properties_obj_.height_offset));
						}
					}
					break;

				case PropertyOrders.PO_HeightScale:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						if (!this.FloatEqual(core_.HeightScale(mtl_id), properties_obj_.height_scale))
						{
							this.ExecuteCommand(new MtlEditorCommandSetHeightScale(core_, mtl_id, properties_obj_.height_scale));
						}
					}
					break;

				case PropertyOrders.PO_EdgeTessHint:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						if (!this.FloatEqual(core_.EdgeTessHint(mtl_id), properties_obj_.edge_tess_hint))
						{
							this.ExecuteCommand(new MtlEditorCommandSetEdgeTessHint(core_, mtl_id, properties_obj_.edge_tess_hint));
						}
					}
					break;

				case PropertyOrders.PO_InsideTessHint:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						if (!this.FloatEqual(core_.InsideTessHint(mtl_id), properties_obj_.inside_tess_hint))
						{
							this.ExecuteCommand(new MtlEditorCommandSetInsideTessHint(core_, mtl_id, properties_obj_.inside_tess_hint));
						}
					}
					break;

				case PropertyOrders.PO_MinTess:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						if (!this.FloatEqual(core_.MinTess(mtl_id), properties_obj_.min_tess))
						{
							this.ExecuteCommand(new MtlEditorCommandSetMinTess(core_, mtl_id, properties_obj_.min_tess));
						}
					}
					break;

				case PropertyOrders.PO_MaxTess:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						if (!this.FloatEqual(core_.MaxTess(mtl_id), properties_obj_.max_tess))
						{
							this.ExecuteCommand(new MtlEditorCommandSetMaxTess(core_, mtl_id, properties_obj_.max_tess));
						}
					}
					break;

				case PropertyOrders.PO_Transparent:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						if (core_.TransparentMaterial(mtl_id) != properties_obj_.transparent)
						{
							this.ExecuteCommand(new MtlEditorCommandSetTransparent(core_, mtl_id, properties_obj_.transparent));
						}
					}
					break;

				case PropertyOrders.PO_AlphaTest:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						if (!this.FloatEqual(core_.AlphaTestMaterial(mtl_id), properties_obj_.alpha_test))
						{
							this.ExecuteCommand(new MtlEditorCommandSetAlphaTest(core_, mtl_id, properties_obj_.alpha_test));
						}
					}
					break;

				case PropertyOrders.PO_SSS:
					if (selected_mtl_id_ > 0)
					{
						uint mtl_id = selected_mtl_id_ - 1;
						if (core_.SSSMaterial(mtl_id) != properties_obj_.sss)
						{
							this.ExecuteCommand(new MtlEditorCommandSetSSS(core_, mtl_id, properties_obj_.sss));
						}
					}
					break;

				default:
					break;
			}
		}

		private void FileNameChanged(string name)
		{
			opened_file_ = name;
			int dot_offset = opened_file_.LastIndexOf('.');
			if (".model_bin" == opened_file_.Substring(dot_offset))
			{
				opened_file_ = opened_file_.Substring(0, dot_offset);
			}

			doc1.Title = opened_file_;
		}

		private string RelativePath(string name)
		{
			if ("" == name)
			{
				return "";
			}
			else
			{
				Uri uri_meshml = new Uri(opened_file_);
				Uri uri_tex = new Uri(name);
				Uri relative_uri = uri_meshml.MakeRelativeUri(uri_tex);
				return relative_uri.ToString();
			}
		}

		public void SelectMeshEntity(uint mesh_id)
		{
			this.ExecuteCommand(new MtlEditorCommandSelectMesh(core_, mesh_id));
			this.UpdateMeshProperties(mesh_id);

			if (0 == mesh_id)
			{
				meshes_[0].SelectedInternal(true);
			}
		}

		public void SelectMaterialEntity(uint mtl_id)
		{
			this.UpdateMaterialProperties(mtl_id);

			if (0 == mtl_id)
			{
				materials_[0].SelectedInternal(true);
			}
		}

		public ReadOnlyCollection<MeshEntityViewModel> ModelRoot
		{
			get
			{
				return meshes_;
			}
		}

		public ReadOnlyCollection<MaterialEntityViewModel> MaterialRoot
		{
			get
			{
				return materials_;
			}
		}

		private void UpdateHistroy()
		{
			undo.IsEnabled = (end_command_index_ > 0);
			redo.IsEnabled = (end_command_index_ < command_history_.Count);
		}

		string HistroyCmdName(int index)
		{
			return command_history_[index].Name();
		}

		private object ExecuteCommand(MtlEditorCommand cmd)
		{
			object ret = cmd.Execute();
			int count = command_history_.Count - end_command_index_;
			if (count > 0)
			{
				command_history_.RemoveRange(end_command_index_, count);
			}
			++ end_command_index_;
			command_history_.Add(cmd);

			this.UpdateHistroy();

			return ret;
		}

		private void Undo()
		{
			//Assert(end_command_index_ != 0);

			-- end_command_index_;
			command_history_[end_command_index_].Revoke();
		}

		private void Redo()
		{
			//Assert(end_command_index_ != command_history_.Count);

			command_history_[end_command_index_].Execute();
			++ end_command_index_;
		}

		private void ClearHistroy()
		{
			command_history_.Clear();
			end_command_index_ = 0;
		}

		private void EditorBGDragEnter(object sender, System.Windows.DragEventArgs e)
		{
			if (!e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop) || (sender == e.Source))
			{
				e.Effects = System.Windows.DragDropEffects.None;
			}
		}

		private void EditorBGDrop(object sender, System.Windows.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
			{
				string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
				this.OpenModel(files[0]);
			}
		}

		float LinearToSRGB(float linear)
		{
			if (linear < 0.0031308f)
			{
				return 12.92f * linear;
			}
			else
			{
				const float ALPHA = 0.055f;
				return (1 + ALPHA) * (float)Math.Pow(linear, 1 / 2.4f) - ALPHA;
			}
		}

		float SRGBToLinear(float srgb)
		{
			if (srgb < 0.04045f)
			{
				return srgb / 12.92f;
			}
			else
			{
				const float ALPHA = 0.055f;
				return (float)Math.Pow((srgb + ALPHA) / (1 + ALPHA), 2.4f);
			}
		}

		private float FloatPtrToMultipiler(float[] clr)
		{
			return Math.Max(Math.Max(Math.Max(clr[0], clr[1]), clr[2]), 1.0f);
		}

		private Color FloatPtrToLDRColor(float[] clr, float multiplier)
		{
			float[] temp = new float[3];
			for (int i = 0; i < 3; ++i)
			{
				temp[i] = LinearToSRGB(clr[i] / multiplier);
			}
			return Color.FromArgb(255,
				(byte)(Math.Max(Math.Min((int)(temp[0] * 255 + 0.5f), 255), 0)),
				(byte)(Math.Max(Math.Min((int)(temp[1] * 255 + 0.5f), 255), 0)),
				(byte)(Math.Max(Math.Min((int)(temp[2] * 255 + 0.5f), 255), 0)));
		}

		private float[] ColorToFloatPtr(Color clr, float multiplier)
		{
			float[] ret = new float[3];
			ret[0] = clr.R / 255.0f;
			ret[1] = clr.G / 255.0f;
			ret[2] = clr.B / 255.0f;
			for (int i = 0; i < 3; ++i)
			{
				ret[i] = SRGBToLinear(ret[i]) * multiplier;
			}
			return ret;
		}

		private bool FloatEqual(float a, float b)
		{
			return Math.Abs(a - b) < 1e-6f;
		}

		private KlayGE.MtlEditorCoreWrapper core_;
		private DateTime last_time_;
		private double frame_;
		private ModelPropertyTypes properties_obj_;
		private uint selected_mesh_id_ = 0;
		private uint selected_mtl_id_ = 0;
		private string opened_file_ = "";

		private bool skinning_ = false;
		private bool fps_camera_ = false;
		private bool line_mode_ = false;
		private bool imposter_mode_ = false;
		private bool play_ = false;

		private readonly ReadOnlyCollection<MeshEntityViewModel> meshes_;
		private readonly ReadOnlyCollection<MaterialEntityViewModel> materials_;

		private List<MtlEditorCommand> command_history_ = new List<MtlEditorCommand>();
		private int end_command_index_ = 0;
	}

	public class DetailModeItemsSource : IItemsSource
	{
		static public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection items;

		public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
		{
			return items;
		}
	}
}
