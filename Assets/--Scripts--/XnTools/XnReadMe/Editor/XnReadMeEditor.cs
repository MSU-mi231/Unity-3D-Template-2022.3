// #define DEBUG_MARKDOWN_EXPORT

// Original file was created by Unity.com
// Modifications to allow \n \t in text by Jeremy G. Bond <jeremy@exninja.com>
// Additional modifications by Jeremy Bond to edit ReadMe files for MI231 at Michigan State University
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace XnTools {

	[CustomEditor( typeof(XnReadMe_SO) )]
	[InitializeOnLoad]
	public class XnReadMeEditor : Editor {
		private const string ProjectMenuHeader     = "Help/MI 231 - ";
		private const string UnityWindowLayoutPath = "--Scripts--/XnTools/UnityWindowLayout.wlt";

		static         string kShowedProjectInfoSessionStateName = "ProjectInfoEditor.showedProjectInfo";

		static float kSpace = 16f;

		static XnReadMeEditor() {
			EditorApplication.delayCall += SelectProjectInfoAutomatically;
		}

		static void SelectProjectInfoAutomatically() {
			if ( !SessionState.GetBool( kShowedProjectInfoSessionStateName, false ) ) {
				var pInfo = SelectProjectInfo();
				SessionState.SetBool( kShowedProjectInfoSessionStateName, true );

				//if ( pInfo && !pInfo.loadedLayout ) {
				//	// LoadLayout();
				//	pInfo.loadedLayout = true;
				//}
			}
		}

		[MenuItem( ProjectMenuHeader + "Load IGDPD4e Window Layout", false, 2)]
		static void LoadBestWindowLayout() {
			var assembly = typeof( EditorApplication ).Assembly;
			var windowLayoutType = assembly.GetType( "UnityEditor.WindowLayout", true );
			// var method = windowLayoutType.GetMethod( "LoadWindowLayout", BindingFlags.Public | BindingFlags.Static );
			var method = windowLayoutType.GetMethod("LoadWindowLayout",
				BindingFlags.Public | BindingFlags.Static,
				null, new Type[] { typeof(string), typeof(bool) }, null);
			// Assets/--Scripts--/XnTools/UnityWindowLayout.wlt
			method?.Invoke( null, new object[] { Path.Combine( Application.dataPath, UnityWindowLayoutPath ), false } );
		}

		[MenuItem( ProjectMenuHeader + "Show Project ReadMe", false, 1 )]
		static XnReadMe_SO SelectProjectInfo() {
			var ids = AssetDatabase.FindAssets( "t:XnReadMe_SO" );
			if ( ids.Length == 1 ) {
				var pInfoObject = AssetDatabase.LoadMainAssetAtPath( AssetDatabase.GUIDToAssetPath( ids[0] ) );

				Selection.objects = new UnityEngine.Object[] { pInfoObject };
				XnReadMe_SO pInfo = (XnReadMe_SO) pInfoObject;
				pInfo.showReadMeEditor = false; // Defaults to not showing the editing interface.

				return pInfo;
			} else if ( ids.Length == 0 ) {
				Debug.Log( "Couldn't find a ReadMe" );
				return null;
			} else {
				Debug.Log( "Found more than 1 ReadMe file" );
				return null;
			}
		}

		/// <summary>
		/// Adds an "Edit..." item to the context menu for InfoComponent
		/// </summary>
		/// <param name="command"></param>
		[MenuItem( "CONTEXT/ReadMe/Edit ReadMe..." )]
		static void EnableEdit( MenuCommand command ) {
			XnReadMe_SO info = (XnReadMe_SO) command.context;
			info.showReadMeEditor = !info.showReadMeEditor;
		}

		protected override void OnHeaderGUI() {
			var pInfo = (XnReadMe_SO) target;
			Init();

			var iconWidth = Mathf.Min( EditorGUIUtility.currentViewWidth / 3f - 20f, pInfo.iconMaxWidth );

			GUILayout.BeginHorizontal( "In BigTitle" );
			{
				GUILayout.Label( pInfo.icon, GUILayout.Width( iconWidth ), GUILayout.Height( iconWidth ) );
				GUILayout.BeginVertical( "In BigTitle", GUILayout.ExpandHeight( true ) );
				{
					if ( pInfo.projectName != null ) {
						string titleString = ReplaceTabsAndNewLines( pInfo.projectName );
						GUILayout.Label( titleString, TitleStyle );
					} else {
						GUILayout.Label( "You must set this projectName", TitleStyle );
					}
					GUILayout.FlexibleSpace();
					GUILayout.Label( $"<b>Author:</b> {pInfo.author}", SubTitleStyle );
					GUILayout.Label( $"<b>Modified:</b> {pInfo.modificationDate}", SubTitleStyle );

					if ( pInfo.showReadMeEditor ) {
						if ( GUILayout.Button( "Finish Editing This ReadMe" ) ) {
							pInfo.showReadMeEditor = false;
							if ( EditorUtility.DisplayDialog( "Update Your Git Repo's ReadMe.md file?",
								    "By default, when you finish editing this __ReadMe__ file, the editor" +
								    " also updates the README.md MarkDown file that you see in GitHub and GitLab." +
								    " When doing so, it prepends the default Unity Template README.md with" +
								    " the ReadMe in this __ReadMe__ asset. This cannot be undone.",
								    "Yes, Update ReadMe.md", "Do NOT Update ReadMe.md" ) ) {
								// Undo.RecordObjects(pInfo, "Reset ReadMe to Defaults");
								ExportReadMeMarkDown( pInfo );
							}
						}
					} else {
						if ( GUILayout.Button( "Edit This ReadMe File" ) ) {
							pInfo.showReadMeEditor = true;
							// Update the date
							pInfo.modificationDate = XnReadMe_SO.currentDate;
						}
					}
				}
				GUILayout.EndVertical();
			}
			GUILayout.EndHorizontal();
		}

		public override void OnInspectorGUI() {
			var pInfo = (XnReadMe_SO) target;
			Init();
			// Start a code block to check for GUI changes
			EditorGUI.BeginChangeCheck();

			// if (pInfo.showReadMeEditor) {
			// 	if ( GUILayout.Button( "Finish Editing This ReadMe" ) ) {
			// 		pInfo.showReadMeEditor = false;
			// 	}
			// 	GUILayout.Space(10);
			// } else {
			// 	if ( GUILayout.Button( "Edit This ReadMe File" ) ) {
			// 		pInfo.showReadMeEditor = true;
			// 	}
			// 	GUILayout.Space(10);
			// }

			if ( pInfo.showReadMeEditor ) {

				GUILayout.Label( "<b>ReadMe Editing Area</b>", HeadingStyle );
				GUILayout.Label( "<i>You can see a preview of what each section will look like below this editor.\n" +
				                 "Edit the <b>Sections</b> below to answer required questions.</i>", BodyStyle );
				GUILayout.Space( 10 );
				DrawDefaultInspector();
				GUILayout.Space( 20 );

				GUILayout.Label( "<b>Button Actions</b>", HeadingStyle );
				// if ( GUILayout.Button( "Export Info to ReadMe.md File for Git" ) ) {
				// 	if ( EditorUtility.DisplayDialog( "Are you sure you want to replace the ReadMe.md file?",
				// 		    "This will replace the ReadMe.md MarkDown file that you see in GitHub and GitLab" +
				// 		    " with information from this file. This cannot be undone.",
				// 		    "Yes, Replace ReadMe.md", "Cancel" ) ) {
				// 		// Undo.RecordObjects(pInfo, "Reset ReadMe to Defaults");
				// 		ExportReadMeMarkDown(pInfo);
				// 	}
				// }
				if ( GUILayout.Button( "Reset ReadMe to Defaults" ) ) {
					if ( EditorUtility.DisplayDialog( "Reset ReadMe to Defaults?",
						    "Are you sure you want to reset this ReadMe file to the default values for" +
						    " MI 231 projects? This cannot be undone.",
						    "Yes, Reset It", "Cancel" ) ) {
						// Undo.RecordObjects(pInfo, "Reset ReadMe to Defaults");
						pInfo.ResetReadMeToDefaults();
					}
				}
				GUILayout.Space( 20 );

				GUILayout.Label( "<b>ReadMe Preview Area</b>", HeadingStyle );
				GUILayout.Space( 10 );
			}

			if ( pInfo.sections != null ) {
				foreach ( var section in pInfo.sections ) {
					if ( !string.IsNullOrEmpty( section.heading ) ) {
						GUILayout.Label( section.heading, HeadingStyle );
					}
					if ( !string.IsNullOrEmpty( section.text ) ) {
						string sTxt = ReplaceTabsAndNewLines( section.text );
						GUILayout.Label( sTxt, BodyStyle );

						// Extract the URLs in the text and make them individual buttons
						List<string> urlList = ExtractUrls( section.text );
						if ( urlList.Count > 0 ) {
							GUILayout.Label( "Links in this section", SubTitleStyle );
						}
						foreach ( string url in urlList ) {
							if ( LinkLabel( new GUIContent( url ) ) ) {
								Application.OpenURL( url );
							}
						}
					}
					// if ( !string.IsNullOrEmpty( section.linkText ) ) {
					// 	if ( LinkLabel( new GUIContent( section.linkText ) ) ) {
					// 		Application.OpenURL( section.url );
					// 	}
					// }
					GUILayout.Space( kSpace );
				}
			}

			if ( EditorGUI.EndChangeCheck() ) {
				EditorUtility.SetDirty( pInfo );
				AssetDatabase.SaveAssets();
			}
		}

		public List<string> ExtractUrls( string text ) {
			var urls = new List<string>();
			if ( string.IsNullOrEmpty( text ) ) {
				return urls;
			}

			var regex = new Regex( @"https?://[^\s]+", RegexOptions.Compiled | RegexOptions.IgnoreCase );
			var matches = regex.Matches( text );

			foreach ( Match match in matches ) {
				urls.Add( match.Value );
			}

			return urls;
		}

		// private const bool   DEBUG_MARKDOWN_EXPORT = false;
		private const string FILE_SEPARATOR      = "\n---\n";
		private const string FILE_SEPARATOR_CRLF = "\r\n---\r\n";

		void ExportReadMeMarkDown( XnReadMe_SO pInfo ) {
			string mdText = pInfo.ToMarkDownString();
#if DEBUG_MARKDOWN_EXPORT
		Debug.LogWarning( mdText );
#endif

			// Import the existing README.md file, if it exists
			string path = "README.md"; // This is at the root level of the Unity project
			string fileText;
			bool useCRLF = false;
			if ( File.Exists( path ) ) {
				StreamReader reader = new StreamReader( path );
				fileText = reader.ReadToEnd();
				reader.Close();

				// Attempt to prepend the file by replacing the first few lines
				int ndx = fileText.IndexOf( FILE_SEPARATOR );
				if ( ndx == -1 ) {
					ndx = fileText.IndexOf( FILE_SEPARATOR_CRLF );
				}
				if ( ndx > 0 ) {
					fileText = fileText.Substring( ndx );
				}
				fileText = mdText + "\n" + fileText;
#if DEBUG_MARKDOWN_EXPORT
			Debug.LogWarning( fileText );
#endif
			} else {
				useCRLF = (Environment.NewLine == "\r\n");
				fileText = mdText + "\n" + (useCRLF ? FILE_SEPARATOR_CRLF : FILE_SEPARATOR);
			}

			using ( StreamWriter writer = new StreamWriter( path, false ) ) {
				writer.Write( fileText );
				writer.Close();
			}

			// Update the file's timestamp to ensure Git recognizes the change
			File.SetLastWriteTimeUtc( path, DateTime.UtcNow );
		}

		string ReplaceTabsAndNewLines( string sIn ) {
			string sOut = sIn.Replace( "\\n", "\n" ).Replace( "\\t", "\t" );
			return sOut;
		}


		bool m_Initialized;

		GUIStyle LinkStyle {
			get { return m_LinkStyle; }
		}

		[SerializeField]
		GUIStyle m_LinkStyle;

		GUIStyle TitleStyle {
			get { return m_TitleStyle; }
		}

		[SerializeField]
		GUIStyle m_TitleStyle;

		GUIStyle SubTitleStyle {
			get { return m_SubTitleStyle; }
		}

		[SerializeField]
		GUIStyle m_SubTitleStyle;

		GUIStyle HeadingStyle {
			get { return m_HeadingStyle; }
		}

		[SerializeField]
		GUIStyle m_HeadingStyle;

		GUIStyle BodyStyle {
			get { return m_BodyStyle; }
		}

		[SerializeField]
		GUIStyle m_BodyStyle;

		void Init() {
			if ( m_Initialized )
				return;

			m_BodyStyle = new GUIStyle( EditorStyles.label );
			m_BodyStyle.wordWrap = true;
			m_BodyStyle.fontSize = 14;
			m_BodyStyle.richText = true;

			m_TitleStyle = new GUIStyle( m_BodyStyle );
			m_TitleStyle.fontSize = 26;
			m_TitleStyle.alignment = TextAnchor.MiddleCenter;

			m_SubTitleStyle = new GUIStyle( m_BodyStyle );
			m_SubTitleStyle.fontSize = 18;
			m_SubTitleStyle.alignment = TextAnchor.MiddleCenter;

			m_HeadingStyle = new GUIStyle( m_BodyStyle );
			m_HeadingStyle.fontSize = 18;

			m_LinkStyle = new GUIStyle( m_BodyStyle );
			m_LinkStyle.wordWrap = false;
			// Match selection color which works nicely for both light and dark skins
			m_LinkStyle.normal.textColor = new Color( 0x00 / 255f, 0x78 / 255f, 0xDA / 255f, 1f );
			m_LinkStyle.stretchWidth = false;

			m_Initialized = true;
		}

		bool LinkLabel( GUIContent label, params GUILayoutOption[] options ) {
			var position = GUILayoutUtility.GetRect( label, LinkStyle, options );

			Handles.BeginGUI();
			Handles.color = LinkStyle.normal.textColor;
			Handles.DrawLine( new Vector3( position.xMin, position.yMax ),
				new Vector3( position.xMax, position.yMax ) );
			Handles.color = Color.white;
			Handles.EndGUI();

			EditorGUIUtility.AddCursorRect( position, MouseCursor.Link );

			return GUI.Button( position, label, LinkStyle );
		}
	}
}

/*


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Reflection;

[CustomEditor(typeof(ReadMe))]
[InitializeOnLoad]
public class ReadMeEditor : Editor {

	static string kShowedReadMeSessionStateName = "ReadMeEditor.showedReadMe";

	static float kSpace = 16f;

	static ReadMeEditor()
	{
		EditorApplication.delayCall += SelectReadMeAutomatically;
	}

	static void SelectReadMeAutomatically()
	{
		if (!SessionState.GetBool(kShowedReadMeSessionStateName, false ))
		{
			var pInfo = SelectReadMe();
			SessionState.SetBool(kShowedReadMeSessionStateName, true);

			if (pInfo && !pInfo.loadedLayout)
			{
				LoadLayout();
				pInfo.loadedLayout = true;
			}
		}
	}

	static void LoadLayout()
	{
		EditorUtility.LoadWindowLayout(Path.Combine(Application.dataPath, "Utilities/»ReadMe/ReadMe.wlt"));
	}

	[MenuItem("Tutorial/ReadMe")]
	static ReadMe SelectReadMe()
	{
		var ids = AssetDatabase.FindAssets("ReadMe t:ReadMe");
		if (ids.Length == 1)
		{
			var pInfoObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(ids[0]));

			Selection.objects = new UnityEngine.Object[]{pInfoObject};

			return (ReadMe)pInfoObject;
		}
		else
		{
			Debug.Log("Couldn't find a pInfo");
			return null;
		}
	}

	protected override void OnHeaderGUI()
	{
		var pInfo = (ReadMe)target;
		Init();

		var iconWidth = Mathf.Min(EditorGUIUtility.currentViewWidth/3f - 20f, pInfo.iconMaxWidth);

		GUILayout.BeginHorizontal("In BigTitle");
		{
			GUILayout.Label(pInfo.icon, GUILayout.Width(iconWidth), GUILayout.Height(iconWidth));
			GUILayout.Label(pInfo.projectName, TitleStyle);
		}
		GUILayout.EndHorizontal();
	}

	public override void OnInspectorGUI()
	{
		var pInfo = (ReadMe)target;
		Init();

		foreach (var section in pInfo.sections)
		{
			if (!string.IsNullOrEmpty(section.heading))
			{
				GUILayout.Label(section.heading, HeadingStyle);
			}
			if (!string.IsNullOrEmpty(section.text))
			{
				GUILayout.Label(section.text, BodyStyle);
			}
			if (!string.IsNullOrEmpty(section.linkText))
			{
				GUILayout.Space(kSpace / 2);
				if (LinkLabel(new GUIContent(section.linkText)))
				{
					Application.OpenURL(section.url);
				}
			}
			GUILayout.Space(kSpace);
		}
	}


	bool m_Initialized;

	GUIStyle LinkStyle { get { return m_LinkStyle; } }
	[SerializeField] GUIStyle m_LinkStyle;

	GUIStyle TitleStyle { get { return m_TitleStyle; } }
	[SerializeField] GUIStyle m_TitleStyle;

	GUIStyle HeadingStyle { get { return m_HeadingStyle; } }
	[SerializeField] GUIStyle m_HeadingStyle;

	GUIStyle BodyStyle { get { return m_BodyStyle; } }
	[SerializeField] GUIStyle m_BodyStyle;

	void Init()
	{
		if (m_Initialized)
			return;
		m_BodyStyle = new GUIStyle(EditorStyles.label);
		m_BodyStyle.wordWrap = true;
		m_BodyStyle.fontSize = 14;

		m_TitleStyle = new GUIStyle(m_BodyStyle);
		m_TitleStyle.fontSize = 26;

		m_HeadingStyle = new GUIStyle(m_BodyStyle);
		m_HeadingStyle.fontSize = 18;
		m_HeadingStyle.fontStyle = FontStyle.Bold;

		m_LinkStyle = new GUIStyle(m_BodyStyle);
		// Match selection color which works nicely for both light and dark skins
		m_LinkStyle.normal.textColor = new Color (0x00/255f, 0x78/255f, 0xDA/255f, 1f);
		m_LinkStyle.stretchWidth = false;

		m_Initialized = true;
	}

	bool LinkLabel (GUIContent label, params GUILayoutOption[] options)
	{
		var position = GUILayoutUtility.GetRect(label, LinkStyle, options);

		Handles.BeginGUI ();
		Handles.color = LinkStyle.normal.textColor;
		Handles.DrawLine (new Vector3(position.xMin, position.yMax), new Vector3(position.xMax, position.yMax));
		Handles.color = Color.white;
		Handles.EndGUI ();

		EditorGUIUtility.AddCursorRect (position, MouseCursor.Link);

		return GUI.Button (position, label, LinkStyle);
	}
}

*/