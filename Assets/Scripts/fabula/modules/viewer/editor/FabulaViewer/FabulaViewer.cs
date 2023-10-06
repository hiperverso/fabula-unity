using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using fabula.modules.manager;
namespace fabula.modules.viewer.editor.fabulaViewer
{
    
    public class FabulaViewer : EditorWindow
    {
        
        private FabulaManager _fabulaManager;

        [MenuItem("Fabula/Fabula Viewer" )]
        public static void ShowEditorWindow()
        {
            EditorWindow editorWindow = GetWindow<FabulaViewer>();
            editorWindow.titleContent = new UnityEngine.GUIContent("Fabula Viewer");
        }


        [MenuItem("Fabula/Load CSV File", priority = 0)]
        static void LoadCSVFile()
        {
            FabulaManager fabManager = GameObject.FindObjectOfType<FabulaManager>();
            if(fabManager == null)
            {
                EditorUtility.DisplayDialog("Select a CSV File", "You need to create a FabulaManager object first", "Ok");
                return;
            }

            string filePath = EditorUtility.OpenFilePanel("Load CSV File", "", "csv");
            if(filePath.Length != 0)
            {
                fabManager.LoadMessages(filePath);
            }


        }

        private void CreateGUI()
        {
            rootVisualElement.Add(new Label("Fabula Viewer"));

            var splitView = new TwoPaneSplitView(0, 120, TwoPaneSplitViewOrientation.Vertical);
            rootVisualElement.Add(splitView);
            
            splitView.Add(TopVerticalPaneElement());
            splitView.Add(BottomVerticalPaneElement());
        }

        private VisualElement TopVerticalPaneElement()
        {
            VisualElement visualElement = new VisualElement();
            visualElement.Add(new Label("Top View"));

            return visualElement;
        }

        private VisualElement BottomVerticalPaneElement()
        {
            VisualElement visualElement = new VisualElement();
            visualElement.Add(new Label("Bottom View"));
            return visualElement;
        }
    }
}