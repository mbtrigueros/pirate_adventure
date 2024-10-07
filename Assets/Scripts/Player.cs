using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase Player. Esta clase se encargará de los comportamientos del jugador, en un principio es un solo archivo pero probablemente abarque más dependiendo de la complejidad. 
// Podemos definir una clase como un "tipo" de objeto. 
// Para crear una clase vamos a usar la palabra reservada "class" y definiremos si es pública (public) o privada (private). 
// En Unity, la mayoría de los scripts que creamos van a heredar de la clase MonoBehaviour. Esta clase es creada por Unity, y facilita muchas cosas a la hora del manejo de un elemento de juego. Por ejemplo, contiene los métodos Start() y Update(), entre otros.
// Que significa que "hereda"? Cuando una clase hereda de otra, esto quiere decir que puede utilizar métodos, campos y variables que definen o pertenecen a dicha clase. Ejemplo: Existe una clase Perro con el método Ladrar(). Tanto un Caniche como un Ovejero Alemán, que serían en este ejmplo distintas clases, van a heredar de la clase Perro y van a poder utilizar el método ladrar. 
public class Player : MonoBehaviour
{
    // Todo lo que vemos antes del método Start() son fields o campos. Estos nos permiten guardar valores que luego vamos a utilizar en nuestro código. 

    // Esto es una lista (List) de jugadores. La sintaxis es List<NombreDeLaClase>. Acá lo que estoy haciendo es instanciando una lista que va a ir agregando los elementos que posean la clase Player.  
    public static List<Player> players = new();

    // La palabra reservada "SerializedField" se utiliza para poder modificar en el editor de Unity el campo. Suele utilizarse para poder diseñar y probar cosas con facilidad, sin tener que meterte en el código cada vez que quieras cambiar algo. En este caso, el campo es de tipo flotante (número decimal) y se llama speed, la velociadad. Por default la puse en 5, pero puede ser modificada como mencioné anteriormente en el editor. La f luego del número es para especificar que el número es de tipo float. 
    [SerializeField] private float speed = 5f;
    
    // En este caso el campo es de tipo Vector3, esto es un Vector que posee 3 valores: rgb o xyz. Significan lo mismo y pueden utilizarse de manera intercambiable. En este caso, el vector va a indicarnos una posición a la cual queremos que nuestro jugador se dirija, por lo tanto tiene sentido entender los valores como xyz. 
    private Vector3 target;

    // Este campo es de tipo booleano, puede ser verdadero o falso. Este tipo de variables suelen usarse para chequear si algo pasó o si se cumple determinada condición para poder luego realizar una acción. En este caso, la usaremos para chequear si el jugador ya se movió. 
    private bool moved = false;

    // El método Start() es un método que provee Unity, y se llama en el primer frame.  
    void Start()
    {
        // Esto agrega mediante el método Add() a la lista que establecimos previamente. This es el elemento a cual está adherido el script, en este caso cada Player.  
        players.Add(this);

        // Acá establecemos inicialmente que la posición target será la misma que la posición del Player. Para eso accedemos al componente transform que pertenece al Player, y a su vez al Vector3 position, que es parte de transform. 
        target = transform.position;
    }

    // El método Update es otro método establecido por Unity, que es llamado una vez por frame. 
    void Update()
    {

        // MOVIMIENTO DEL JUGADOR

        // La instrucción "if" es un condicional, esto es, una instrucción que va a corrobar que se cumpla cierta condición o condiciones antes de ejecutar el código en su interior. Dentro de los paréntesis pondremos las condiciones que requerimos y luego el código dentro de las llaves {}.
        // Acá estamos comprobando tres condiciiones: 
        // por un lado, si el usuario está apretando el botón izquierdo del mouse. Para chequear esto, utilizamos la clase otorgada por Unity "Input", y el método "GetMouseButtonDown()" que nos va a devolver true si el usuario está apretando en ese frame el botón indicado. Dentro de los () podemos poner 0 para indicar el botón izquierdo o 1 para indicar el botón derecho del mouse. 
        // Segunda condición: Si el campo moved, creado por nosotros, es falso. Esto puede escribirse como !moved o como moved == false. Es decir, si el jugador NO se ha movido. 
        // Última condición: acá hacemos uso de la lista: mediante el método IndexOf() vamos a corroborar si el índice de el objeto que posee este script, en este caso cada Player, es 0. Qué es el índice? El índice es la posición en la lista. Las listas en C# empiezan con el número 0. En este caso, el primer jugador va a tener la posición 0, y el segundo va a tener la posición o índice 1. Compruebo esto para decidir qué jugador va a moverse primero.  
        // Todas las condiciones están unidas mediante && para indicar AND o Y. Esto significa que TODAS las condiciones deben cumplirse para poder ejecutar el código dentro del if. 
        if(Input.GetMouseButtonDown(0) && !moved && players.IndexOf(this) == 0) {

            // Si todas estas condiciones se cumplen, ejecuto el código:
            // Acá establezo que la posición o Vector3 del campo target va a ser igual a la posición del mouse. 
            // Para esto investigando descubrí que necesito transformar el espacio de la pantalla al espacio del "mundo" es decir del juego. Esto lo hago mediante el método ScreenToWorldPoint, que pertenece al componente Camera. Main en este caso se refiere a la MainCamera que tenemos en nuestro editor de Unity. Debe haber una Cámara establecida como Main o principal para que esto funcione. Dentro de los paréntesis le pasamos la posición del mouse, nuevamente utilizando la clase Input.
            target = Camera.main.ScreenToWorldPoint( Input.mousePosition );

            // Acá lo único que hago es establecer la posición z del target igual a la del jugador, puesto que al ser un juego 2d la posición z no es relevante, y para estar seguros es mejor que esté en un valor estable. 
            target.z = transform.position.z;

            // Luego cambio el campo "moved" a true, para avisar que el jugador se movió. 
            moved = true;
        }

            // Acá, fuera del if, utilizo el método MoveTowards de la estructura Vector3 de Unity para calcular la distancia que hay entre la posición del jugador y la posición del target. Luego me muevo hacia esa posición a la velocidad establecida previamente multiplicada por el tiempo.
            // Con esta línea terminamos todo lo que refiere al MOVIMIENTO del jugador. 
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);


            // CAMBIO DE TURNO

            // Utilizamos nuevamente la instrucción if para comprobar determinadas condiciones antes de ejecutar el código:
            // Chequeamos primero que la posición del jugador sea IGUAL a la de nuestra target. Osea, que el jugador llegó a destino. 
            // Luego confirmamos también que efectivamente se haya movido: recordemos que al principio la posición de target y del jugador es la misma, por lo que si no chequeamos si se movió puede haber errores (creanme los hubo :) ) 
            // Finalmente confirmamos una vez más que el índice es 0. 
            if(transform.position == target && moved && players.IndexOf(this) == 0)
            { 

            // Para diferenciar al jugador que se movió le aplico temporalmente el color rojo. Esto lo hago llamando al componente SpriteRenderer que contienen todos los game objects de tipo Sprite. 
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;

                // Acá mediante la instrucción de iteración "foreach" recorro todos los jugadores en la lista.
                foreach(Player player in players) {
                    // Si el jugador NO ES el que contiene el código, es decir, es el OTRO. 
                    if (player != this) {

                        // Cambio su variable moved a false, para que cuando cambiemos de turno pueda moverse. 
                        player.moved = false;

                        // Cambio su color a blanco, para volverlo al estado "original". 
                        player.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }

                // Después, mediante el método Reverse() INVIERTO la lista, por lo que el jugador en posición 1 pasa a la posición 0 y puede realizar su movimiento. De esta manera logro que una vez hecho el movimiento de un jugador, pase automáticamente al siguiente. 
                // ACLARACIÓN: esto probablemente sea transitorio, porque no es sostenible si agregamos más jugadores. Pero por lo pronto funciona.                                 
                players.Reverse();
            }
    }

    private void OnTriggerEnter2D(Collider2D other) {
            print("Touched Point");
        
    }
    
}
