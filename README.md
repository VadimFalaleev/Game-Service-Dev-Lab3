# Разработка игровых сервисов
Отчет по лабораторной работе #3 выполнил(а):
- Фалалеев Вадим Эдуардович
- РИ-300012

Отметка о выполнении заданий (заполняется студентом):

| Задание | Выполнение | Баллы |
| ------ | ------ | ------ |
| Задание 1 | * | 60 |
| Задание 2 | * | 20 |
| Задание 3 | * | 20 |

знак "*" - задание выполнено; знак "#" - задание не выполнено;

Работу проверили:
- к.т.н., доцент Денисов Д.В.
- к.э.н., доцент Панов М.А.
- ст. преп., Фадеев В.О.

Структура отчета

- Данные о работе: название работы, фио, группа, выполненные задания.
- Цель работы.
- Задание 1.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 2.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 3.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Выводы.

## Цель работы
Интеграция интерфейса пользователя в разрабатываемое интерактивное приложение.

## Задание 1
### Используя видео-материалы практических работ 1-2 повторить реализацию игровых механик:

– 1 Практическая работа «Реализация механизма ловли объектов».

– 2 Практическая работа «Реализация графического интерфейса с добавлением счетчика очков».

Ход работы:

- Реализуем движение сферы за указателем мыши. Для этого создадим скрипт EnergyShield, поставим его на объект EnergyShield и напишем следующий код.

```c#

using UnityEngine;

public class EnergyShield : MonoBehaviour
{

    void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }
}

```

- Следующим шагом сделаем уничтожение яиц при столкновении со сферой. Добавим в прошлый скрипт новый метод, реализующий данные действия.

```c#

private void OnCollisionEnter(Collision collision)
{
    GameObject collided = collision.gameObject;
    if (collided.tag == "Dragon Egg")
        Destroy(collided);
}

```

- Последним действием первого урока будет добавление интерфейса для будущего подсчета очков. Создаем объект Canvas, нажимая в списке сцены ПКМ, UI -> Text -> TextMeshPro. Переименуем объект внутри Canvas на Score. В компоненте Canvas объекта Canvas поменяем Render Mode на Screen Space - Camera, и в Render Camera поставим объект Main Camera со сцены. Plane Distance изменим на 10. 

![image](https://user-images.githubusercontent.com/54228342/196152589-e958f94d-428f-4142-b716-b2b3d75888c1.png)

- В компоненте Rect Transform объекта Score поставим якорь на верхний правый угол, Pos X и Pos Y на -50, Width на 120, Height на 50. В комопненте TextMeshPro - Text (UI) запишем текст "Score:", а Font Size поставим на 32.

![image](https://user-images.githubusercontent.com/54228342/196153473-c04ef97e-2440-43ca-bb5e-76d83a952d74.png)

- После всех совершенных действий посмотрим на итог первого видео.

![Видео 17-10-2022 163329](https://user-images.githubusercontent.com/54228342/196167243-849818b6-6454-412f-a274-0ce0f243310b.gif)

- Перейдем к практике во втором видео. Продолжим работать с пользовательским интерфейсом, сделав счетчик очков рабочим. Вернемся в скрипт EnergyShield. Подключим новую библиотеку и добавим метод Start. Начало скрипта будет выглядить следующим образом:

```c#

using UnityEngine;
using TMPro;

public class EnergyShield : MonoBehaviour
{
    public TextMeshProUGUI scoreGT;

    private void Start()
    {
        GameObject scoreGO = GameObject.Find("Score");
        scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();
        scoreGT.text = "0";
    }
    
    ...

```

- Теперь в конец метода OnCollisionEnter добавим строки, чтобы счет обновлялся при поимке яиц:

```c#

int score = int.Parse(scoreGT.text);
score += 1;
scoreGT.text = score.ToString();

```

- Зайдем в скрипт DragonEgg и там в методе Update добавим строки внутри условия:

```c#

DragonPicker apScript = Camera.main.GetComponent<DragonPicker>();
apScript.DragonEggDestroyed();

```

- После этого заходим в скрипт DragonPicker, создавая новый метод:

```c#

public void DragonEggDestroyed()
{
    GameObject[] tDragonEggArray = GameObject.FindGameObjectsWithTag("Dragon Egg");
    foreach (GameObject tGO in tDragonEggArray)
        Destroy(tGO);
}

```

- Посмотрим на результат работы после второго видео. Счетчик работает и все яйца удаляются, если одно пролетело мимо сферы.

![Видео 17-10-2022 175315](https://user-images.githubusercontent.com/54228342/196182597-fb6f8844-0a6e-4c35-bd06-be1adbe44afe.gif)

## Задание 2
### Используя видео-материалы практических работ 3-4 повторить реализацию игровых механик:

– 3 Практическая работа «Уменьшение жизни. Добавление текстур».

– 4 Практическая работа «Структурирование исходных файлов в папке».

Ход работы:

- Добавим уменьшение нашего здоровья, которым является количество наших щитов, и перезагрузку уровня при проигрыше. Зайдем в скрипт DragonPicker и добавим новые строки в код(они выделены комментариями).

```c#

using System.Collections.Generic; // new
using UnityEngine;
using UnityEngine.SceneManagement; // new

public class DragonPicker : MonoBehaviour
{
    public GameObject energyShieldPrefab;
    public int numEnergyShield = 3;
    public float energyShieldBottomY = -6;
    public float energyShieldRadius = 1.5f;
    public List<GameObject> shieldList; // new

    private void Start()
    {
        shieldList = new(); // new

        for (int i = 1; i <= numEnergyShield; i++)
        {
            GameObject tShieldGo = Instantiate(energyShieldPrefab);
            tShieldGo.transform.position = new(0, energyShieldBottomY, 0);
            tShieldGo.transform.localScale = new(1 * i, 1 * i, 1 * i);
            shieldList.Add(tShieldGo); // new
        }
    }

    public void DragonEggDestroyed()
    {
        GameObject[] tDragonEggArray = GameObject.FindGameObjectsWithTag("Dragon Egg");
        foreach (GameObject tGO in tDragonEggArray)
            Destroy(tGO);

        int shieldIndex = shieldList.Count - 1; // new
        GameObject tShieldGo = shieldList[shieldIndex]; // new
        shieldList.RemoveAt(shieldIndex); // new
        Destroy(tShieldGo); // new
        
        if (shieldList.Count == 0) // new
            SceneManager.LoadScene("_0Scene"); // new
    }
}

```



## Задание 3
### Используя видео-материалы практических работ 5 повторить реализацию игровых механик:

– 5 Практическая работа «Интеграция игровых сервисов в готовое приложение».

Ход работы:


## Выводы
