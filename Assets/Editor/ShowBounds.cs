using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Transform))]
public class ShowBounds : Editor {
	
	bool check = true;
	
    public override void OnInspectorGUI() {

        Transform t = (Transform)target;

        // Replicate the standard transform inspector gui
        EditorGUIUtility.LookLikeControls();
        EditorGUI.indentLevel = 0;
        Vector3 position = EditorGUILayout.Vector3Field("Position", t.localPosition);
        Vector3 eulerAngles = EditorGUILayout.Vector3Field("Rotation", t.localEulerAngles);
        Vector3 scale = EditorGUILayout.Vector3Field("Scale", t.localScale);
        //EditorGUIUtility.LookLikeInspector();
		
		
        if (GUI.changed) {
			Vector3 targetPos = new Vector3(t.localPosition.x, t.localPosition.y, t.localPosition.z);
			Object[] selected = GetSelectedTransforms();
			Undo.RegisterUndo(selected, "Transform Change");
			
			foreach (Transform trsfrm in selected) {
				//trsfrm.localPosition = FixIfNaN(position);
				//trsfrm.localEulerAngles = FixIfNaN(eulerAngles);
				//trsfrm.localScale = FixIfNaN(scale);
				
				if(targetPos.x != position.x){
					trsfrm.localPosition = new Vector3(FixIfNaN2(position.x), trsfrm.localPosition.y, trsfrm.localPosition.z);
				}
				if(targetPos.y != position.y){
					trsfrm.localPosition = new Vector3(trsfrm.localPosition.x, FixIfNaN2(position.y), trsfrm.localPosition.z);
				}
				if(targetPos.z != position.z){
					trsfrm.localPosition = new Vector3(trsfrm.localPosition.x, trsfrm.localPosition.y, FixIfNaN2(position.z));
				}
				
				
				 //t.localPosition = FixIfNaN(position);
           		t.localEulerAngles = FixIfNaN(eulerAngles);
           		t.localScale = FixIfNaN(scale);
			}

           
        }
		
		EditorGUILayout.Space();
		
		Bounds bound = new Bounds(t.position, Vector3.zero);
		if(t.gameObject.renderer){
			bound.Encapsulate(t.gameObject.renderer.bounds);
		}
		
		check = EditorGUILayout.Toggle("Include children", check);
		int total = 0;
		if(check){
			foreach ( Transform child in t ) {
				total++;
				if(child.gameObject.renderer){
					bound.Encapsulate(child.gameObject.renderer.bounds);
				}
			}
		}
		
		EditorGUILayout.LabelField("Bounds", bound.size.ToString());
		
		
		/////////// mesh tris and verts
		EditorGUILayout.Space();
		
		int triangles = 0;
        int vertices = 0;
        int meshCount = 0;
       
        foreach (GameObject go in Selection.GetFiltered(typeof(GameObject), SelectionMode.TopLevel))
        {
            Component[] skinnedMeshes = go.GetComponentsInChildren(typeof(SkinnedMeshRenderer)) ;
            Component[] meshFilters = go.GetComponentsInChildren(typeof(MeshFilter));

            ArrayList totalMeshes = new ArrayList(meshFilters.Length + skinnedMeshes.Length);

            for (int meshFiltersIndex = 0; meshFiltersIndex < meshFilters.Length; meshFiltersIndex++)
            {
                MeshFilter meshFilter = (MeshFilter)meshFilters[meshFiltersIndex];
                totalMeshes.Add(meshFilter.sharedMesh);
            }

            for (int skinnedMeshIndex = 0; skinnedMeshIndex < skinnedMeshes.Length; skinnedMeshIndex++)
            {
                SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)skinnedMeshes[skinnedMeshIndex];
                totalMeshes.Add(skinnedMeshRenderer.sharedMesh);
            }

            for (int i = 0; i < totalMeshes.Count; i++)
            {
                Mesh mesh = totalMeshes[i] as Mesh;
                if (mesh == null)
                {
                    Debug.LogWarning("You have a missing mesh in your scene.");
                    continue;
                }
                vertices += mesh.vertexCount;
                triangles += mesh.triangles.Length / 3;
                meshCount++;
            }
        }
		
		EditorGUILayout.LabelField("Tris: " + triangles.ToString() + " - Verts: " + vertices.ToString() + "  - Total Children: " + total.ToString());
    }

    private Vector3 FixIfNaN(Vector3 v) {
        if (float.IsNaN(v.x)) {
            v.x = 0;
        }
        if (float.IsNaN(v.y)) {
            v.y = 0;
        }
        if (float.IsNaN(v.z)) {
            v.z = 0;
        }
        return v;
    }
	
	  private float FixIfNaN2(float v) {
        if (float.IsNaN(v)) {
            v = 0;
        }
        return v;
    }
	
	
	private Object[] GetSelectedTransforms() {
        return Selection.GetFiltered(typeof(Transform), SelectionMode.Unfiltered | SelectionMode.ExcludePrefab | SelectionMode.TopLevel);
    }

}