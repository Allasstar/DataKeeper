# DataKeeper

## Reactive<T>

The `Reactive<T>` class is a generic class that represents a reactive variable of type `T`. It allows you to track changes to its value and invoke corresponding events when the value is modified.

## ReactivePref<T>

The ReactivePref<T> class is a generic class that represents a reactive preference variable of type T in Unity. It provides a convenient way to store and retrieve preferences using PlayerPrefs while also supporting automatic saving and loading.

## DataFile<T>

The DataFile<T> class is a generic class that provides functionality to save and load data of type T to a file using different serialization formats such as Binary, XML, or JSON.

## Optional<T>

The Optional<T> struct represents an optional value of type T. It allows you to store a value of type T along with a flag indicating whether the value is enabled or not.

## Register<TValue>
The Register<TValue> class is a generic class that inherits from the Container<TValue> class. It provides functionality to register and store values of type TValue using a string identifier or the type name.

## RegisterActivator<TValue>
The RegisterActivator<TValue> class is a generic class that inherits from the Container<TValue> class. It provides additional functionality to instantiate and register values of type TValue using either Activator.CreateInstance<T>() or by instantiating a Component in a GameObject.

## Registrar
The Registrar class is a MonoBehaviour that serves as a registration system for components. It uses the Register<Component> class to store and manage registered components.

## SelectUIElementEditor
The SelectUIElementEditor class is an editor script that allows you to select UI elements in the Unity Editor by pressing the Tab key.

## SO (ScriptableObject)
The SO class is an abstract class that extends the ScriptableObject class provided by Unity. It serves as a base class for creating custom ScriptableObject classes.

The SO script is a base class for ScriptableObjects in Unity, providing two important functionalities: the Initialize() method and the Save() method.

The Initialize() method is meant to be overridden in derived classes. It allows you to define custom initialization logic for your ScriptableObjects. This method will be automatically called when the game starts or when the ScriptableObject is created in the Unity Editor. You can use the Initialize() method to instantiate objects, register the ScriptableObject with some data, or perform any other setup tasks specific to your ScriptableObject.

The Save() method is only available in the Unity Editor and is marked with the [ContextMenu("Save")] attribute. This method is designed to be used during development to manually save changes made to a ScriptableObject. When you right-click on an instance of the ScriptableObject in the Unity Editor, a context menu will appear, and selecting the "Save" option will invoke the Save() method. Inside the Save() method, the ScriptableObject is marked as dirty using EditorUtility.SetDirty(this), indicating that it has been modified. Then, AssetDatabase.SaveAssets() is called to save the changes to disk. This allows you to control when and how the changes to the ScriptableObject are saved without relying solely on automatic serialization.

In summary, the SO script provides a way to initialize ScriptableObjects with custom logic using the Initialize() method, and it allows manual saving of changes made to ScriptableObjects using the Save() method in the Unity Editor during development.
