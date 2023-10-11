using Elements;
using Elements.Geometry;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MakeActions
{
  public static class MakeActions
  {
    /// <summary>
    /// The MakeActions function.
    /// </summary>
    /// <param name="model">The input model.</param>
    /// <param name="input">The arguments to the execution.</param>
    /// <returns>A MakeActionsOutputs instance containing computed results and the model with any new elements.</returns>
    public static MakeActionsOutputs Execute(Dictionary<string, Model> inputModels, MakeActionsInputs input)
    {
      var output = new MakeActionsOutputs();

      var scope = new ViewScope()
      {
        BoundingBox = new BBox3((-50, -50), (50, 50, 50)), // default extents of the view
        ClipWithBoundingBox = false,
        Camera = new Camera(default, CameraNamedPosition.Top, CameraProjection.Orthographic),
        LockRotation = true,
        Name = "Edit Envelope",
        FunctionVisibility = new Dictionary<string, string> {
                            {"Site", "hidden"},
                        },
        Actions = new List<object> {
                        // We haven't made classes for these objects yet, so you'll have to use JObjects to represent them.
                        // Note Action
                        new JObject {
                          ["type"] = "note", // action type
                          ["instruction"] =  // contents of note / what is displayed to user
                            "### This is a markdown note\n"+
                            "- With a list\n"+
                            "- Of items\n"+
                            "\n"+
                            "And an image:\n"+
                            "![alt text](https://hypar.io/img/hypar_logo.f17a5fcb.svg)",
                        },
                        // Add Function Action
                      new JObject {
                        ["type"] = "add-function", // Action type
                        ["id"] = "add-envelope-function", // any unique name is fine
                        ["instruction"] = "Add Envelope By Sketch", // what is displayed to the user 
                        ["functionId"] = "0c8d0526-9490-4d53-896b-88a1515de583", // Id of function to add
                        },
                         new JObject {
                        ["type"] = "add-function", // Action type
                        ["id"] = "add-space-planning-function", // any unique name is fine
                        ["instruction"] = "Add Space Planning", // what is displayed to the user 
                        ["functionId"] = "adc507cb-6005-41c5-a781-ccdd9dfc0956", // Id of function to add
                        },
                      // Function Input Action
                      new JObject {
                        ["type"] = "function-input", // Action type
                        ["id"] = "envelope-inputs", // unique id for action
                        ["instruction"] = "Envelope Settings", // What is displayed to the user
                        ["function"] = JObject.FromObject(new FunctionIdentifier { // Function to display inputs for
                          FunctionId = "0c8d0526-9490-4d53-896b-88a1515de583"
                        }),
                        ["inputNames"] = new JArray { // optional — which inputs show
                          "Perimeter",
                          "Building Height"
                        }
                      },
                      new JObject {
                        ["type"] = "function-override", // Action type
                        ["id"] = "spaces-override", // unique id for action
                        ["instruction"] = "Override Spaces", // What is displayed to the user
                        ["function"] = JObject.FromObject(new FunctionIdentifier { // function to show overrides for. These function identifiers can use a function ID or a model output name.
                          ModelOutput = "Space Planning Zones"
                        }),
                        ["overridePaths"] = new JArray {
                          "Spaces" // Name of the override(s) you want to expose
                        }
                      },
                      // Import File Action
                      new JObject {
                        ["type"] = "import-file", // Action type
                        ["id"] = "import-dxf", // unique id for action
                        ["instruction"] = "Import DXF", // What is displayed to the user
                        ["extensions"] = ".dxf", // supported import file types
                      },
                      // Restore Snapshot Action
                      new JObject {
                        ["type"] = "restore-snapshot", // Action type
                        ["id"] = "restore-some-snapshot", // unique id for action
                        ["instruction"] = "Restore Some Snapshot", // What is displayed to the user
                        ["snapshotName"] = "Some Snapshot" // The name of the snapshot to restore
                      }
                  }
      };
      output.Model.AddElements(scope);
      return output;
    }
  }
}