# Miris.TemplateEngine

With no external dependency, this component aims to process a Razor-like string template.

## How to use:

1. Define a template:
```c#
var template = @"
        Hello @user.name!

        @if (@user.showId == true) { Your ID is: @user.id. }

        Mail addresses: 
        @foreach(var email in user.Emails) 
        {
            @email.Value
        }
        ";
```


2. Define a model to bind:
```c#
var model = new {
    user = new {
        name = "Joseph Climber",
        id = 12345,
        showId = true,
        Emails = new List<object>()
        {
            new { Value = "foo@bar.com" },
            new { Value = "bar@foo.com" },
        }
    }
};
```

3. Bind:
```c#
ITemplateEngine engine = new RazorAlikeEngine();

string result = engine.Run(template, model);
```

4. Result:

```c#
Hello Joseph Climber!

Your ID is: 12345.

Mail addresses: 

    foo@bar.com
    bar@foo.com
```

## Support:
- ✅ if statement (single-line)
- ❌ if statement (multiline)
- ❌ for loop statement (single-line)
- ✅ for loop statement (multiline)